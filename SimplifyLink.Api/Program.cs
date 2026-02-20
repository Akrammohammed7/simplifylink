using Microsoft.EntityFrameworkCore;
using SimplifyLink.Api.Data;
using SimplifyLink.Api.Models;
using Microsoft.Extensions.Caching.Memory;


var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=simplifylink.db"));


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}


// Enable Swagger
app.UseSwagger();
app.UseSwaggerUI();

if (!app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
}


app.UseAuthorization();

app.MapControllers();

app.MapGet("/{shortCode}", async (
    string shortCode,
    AppDbContext context,
    HttpContext http,
    IMemoryCache cache) =>
{
    var cacheKey = $"shortlink:{shortCode}";

    // 1) Try cache first
    if (cache.TryGetValue(cacheKey, out (int linkId, string originalUrl) cached))
    {
        // Increment ClickCount in DB (no need to fetch the row)
        await context.ShortLinks
            .Where(x => x.Id == cached.linkId)
            .ExecuteUpdateAsync(s =>
                s.SetProperty(x => x.ClickCount, x => x.ClickCount + 1));

        // Log click event
        context.ClickEvents.Add(new ClickEvent
        {
            ShortLinkId = cached.linkId,
            UserAgent = http.Request.Headers.UserAgent.ToString(),
            IpAddress = http.Connection.RemoteIpAddress?.ToString()
        });

        await context.SaveChangesAsync();
        return Results.Redirect(cached.originalUrl);
    }

    // 2) Cache miss → hit DB
    var link = await context.ShortLinks
        .FirstOrDefaultAsync(x => x.ShortCode == shortCode);

    if (link is null)
        return Results.NotFound("Short code not found");

    // Store in cache for next time
    cache.Set(cacheKey, (link.Id, link.OriginalUrl), new MemoryCacheEntryOptions
    {
        SlidingExpiration = TimeSpan.FromMinutes(30),
        AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6)
    });

    // Normal DB update + log
    link.ClickCount += 1;

    context.ClickEvents.Add(new ClickEvent
    {
        ShortLinkId = link.Id,
        UserAgent = http.Request.Headers.UserAgent.ToString(),
        IpAddress = http.Connection.RemoteIpAddress?.ToString()
    });

    await context.SaveChangesAsync();

    return Results.Redirect(link.OriginalUrl);
});

app.Run();
