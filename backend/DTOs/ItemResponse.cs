using CampusLostFound.Api.Models;

namespace CampusLostFound.Api.DTOs;

public class ItemResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public DateOnly DateReported { get; set; }
    public ItemType Type { get; set; }
    public ItemStatus Status { get; set; }
    public string ContactName { get; set; } = string.Empty;
    public string ContactEmail { get; set; } = string.Empty;

    public static ItemResponse FromEntity(LostFoundItem item) => new()
    {
        Id = item.Id,
        Title = item.Title,
        Description = item.Description,
        Category = item.Category,
        Location = item.Location,
        DateReported = item.DateReported,
        Type = item.Type,
        Status = item.Status,
        ContactName = item.ContactName,
        ContactEmail = item.ContactEmail
    };
}
