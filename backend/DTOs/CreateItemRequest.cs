using System.ComponentModel.DataAnnotations;
using CampusLostFound.Api.Models;

namespace CampusLostFound.Api.DTOs;

// No Status property on purpose: clients cannot set the initial status,
// it always defaults to ACTIVE in the controller.
public class CreateItemRequest
{
    [Required, NotBlank]
    public string? Title { get; set; }

    [Required, NotBlank]
    public string? Description { get; set; }

    [Required, NotBlank]
    public string? Category { get; set; }

    [Required, NotBlank]
    public string? Location { get; set; }

    [Required]
    public DateOnly? DateReported { get; set; }

    [Required]
    public ItemType? Type { get; set; }

    [Required, NotBlank]
    public string? ContactName { get; set; }

    [Required, EmailAddress]
    public string? ContactEmail { get; set; }
}
