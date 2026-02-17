using System.ComponentModel.DataAnnotations;

namespace SimplifyLink.Api.Models;

public class ClickEvent
{
    public int Id { get; set; }

    [Required]
    public int ShortLinkId { get; set; }

    public ShortLink? ShortLink { get; set; }

    public DateTime ClickedAtUtc { get; set; } = DateTime.UtcNow;

    [MaxLength(512)]
    public string? UserAgent { get; set; }

    [MaxLength(64)]
    public string? IpAddress { get; set; }
}
