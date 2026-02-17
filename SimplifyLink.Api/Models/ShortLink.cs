using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SimplifyLink.Api.Models;

public class ShortLink
{
    public int Id { get; set; }

    [Required]
    [MaxLength(2048)]
    public string OriginalUrl { get; set; } = string.Empty;

    [Required]
    [MaxLength(32)]
    public string ShortCode { get; set; } = string.Empty;

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

    public long ClickCount { get; set; } = 0;

    public List<ClickEvent> ClickEvents {get; set; } = new();
}
