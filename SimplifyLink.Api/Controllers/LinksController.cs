using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimplifyLink.Api.Data;
using SimplifyLink.Api.Models;

namespace SimplifyLink.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LinksController : ControllerBase
{
    private readonly AppDbContext _context;

    public LinksController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("health")]
    public IActionResult Health()
    {
        return Ok(new { status = "ok", service = "SimplifyLink.Api" });
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateShortLinkRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Generate unique short code (retry if collision)
        string shortCode = "";
        for (var attempts = 0; attempts < 5; attempts++)
        {
            shortCode = Guid.NewGuid().ToString("N")[..6];

            var exists = await _context.ShortLinks.AnyAsync(x => x.ShortCode == shortCode);
            if (!exists)
                break;
        }

        if (string.IsNullOrWhiteSpace(shortCode) ||
            await _context.ShortLinks.AnyAsync(x => x.ShortCode == shortCode))
        {
            return StatusCode(500, new { message = "Could not generate a unique short code. Try again." });
        }

        var link = new ShortLink
        {
            OriginalUrl = request.OriginalUrl,
            ShortCode = shortCode
        };

        _context.ShortLinks.Add(link);
        await _context.SaveChangesAsync();

       
        var baseUrl = $"{Request.Scheme}://{Request.Host}";

        return Ok(new
        {
            link.Id,
            link.OriginalUrl,
            link.ShortCode,
            ShortUrl = $"{baseUrl}/{shortCode}"
        });
    }

    [HttpGet("r/{shortCode}")]
    public async Task<IActionResult> RedirectToOriginal(string shortCode)
    {
        var link = await _context.ShortLinks
            .FirstOrDefaultAsync(x => x.ShortCode == shortCode);

        if (link is null)
            return NotFound(new { message = "Short code not found" });

        link.ClickCount += 1;

        var click = new ClickEvent
        {
            ShortLinkId = link.Id,
            UserAgent = Request.Headers.UserAgent.ToString(),
            IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString()
        };

        _context.ClickEvents.Add(click);

        await _context.SaveChangesAsync();

        return Redirect(link.OriginalUrl);
    }

    [HttpGet("{id}/stats")]
    public async Task<IActionResult> GetStats(int id)
    {
        var link = await _context.ShortLinks
            .Include(x => x.ClickEvents)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (link is null)
            return NotFound();

        var last7Days = link.ClickEvents
            .Where(e => e.ClickedAtUtc >= DateTime.UtcNow.AddDays(-7))
            .GroupBy(e => e.ClickedAtUtc.Date)
            .Select(g => new
            {
                Date = g.Key,
                Count = g.Count()
            })
            .OrderBy(x => x.Date)
            .ToList();

        return Ok(new
        {
            link.Id,
            link.ShortCode,
            link.OriginalUrl,
            link.ClickCount,
            TotalEvents = link.ClickEvents.Count,
            Last7Days = last7Days
        });
    }

    [HttpGet("top")]
    public async Task<IActionResult> GetTopLinks([FromQuery] int limit = 5)
    {
        // safety bounds
        if (limit < 1) limit = 1;
        if (limit > 50) limit = 50;

        // ✅ Build public URL dynamically (works on Render + locally)
        var baseUrl = $"{Request.Scheme}://{Request.Host}";

        var top = await _context.ShortLinks
            .OrderByDescending(l => l.ClickCount)
            .ThenByDescending(l => l.CreatedAtUtc)
            .Take(limit)
            .Select(l => new
            {
                l.Id,
                l.OriginalUrl,
                l.ShortCode,
                l.ClickCount,
                l.CreatedAtUtc,
                ShortUrl = baseUrl + "/" + l.ShortCode
            })
            .ToListAsync();

        return Ok(top);
    }
}
