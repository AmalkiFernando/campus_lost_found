using System.ComponentModel.DataAnnotations;

namespace CampusLostFound.Api.DTOs;

// Follows the standard DataAnnotations convention: null is considered valid here
// (pair with [Required] when the field is mandatory) — this attribute only rejects
// empty/whitespace-only strings.
public sealed class NotBlankAttribute : ValidationAttribute
{
    public NotBlankAttribute() : base("The {0} field must not be empty or whitespace.")
    {
    }

    public override bool IsValid(object? value)
    {
        if (value is null)
        {
            return true;
        }

        return value is string s && !string.IsNullOrWhiteSpace(s);
    }
}
