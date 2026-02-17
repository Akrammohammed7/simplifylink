using System.ComponentModel.DataAnnotations;

namespace SimplifyLink.Api.Models;

public class CreateShortLinkRequest
{
    [Required]
    [Url]
    public string OriginalUrl { get; set; } = string.Empty;
}
