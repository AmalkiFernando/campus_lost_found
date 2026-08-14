using System.ComponentModel.DataAnnotations;
using CampusLostFound.Api.Models;

namespace CampusLostFound.Api.DTOs;

// All fields optional: only properties the client actually sends are applied.
// Primary use case is { "status": "RETURNED" }, but any subset of fields can be updated.
public class UpdateItemRequest
{
    [NotBlank]
    public string? Title { get; set; }

    [NotBlank]
    public string? Description { get; set; }

    [NotBlank]
    public string? Category { get; set; }

    [NotBlank]
    public string? Location { get; set; }

    public DateOnly? DateReported { get; set; }

    public ItemType? Type { get; set; }

    public ItemStatus? Status { get; set; }

    [NotBlank]
    public string? ContactName { get; set; }

    [EmailAddress]
    public string? ContactEmail { get; set; }
}
