using System.ComponentModel.DataAnnotations;

namespace CampusLostFound.Api.Models;

public class LostFoundItem
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    public string Category { get; set; } = string.Empty;

    [Required]
    public string Location { get; set; } = string.Empty;

    [Required]
    public DateOnly DateReported { get; set; }

    [Required]
    public ItemType Type { get; set; }

    [Required]
    public ItemStatus Status { get; set; } = ItemStatus.ACTIVE;

    [Required]
    public string ContactName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string ContactEmail { get; set; } = string.Empty;
}
