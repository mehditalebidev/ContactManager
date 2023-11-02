using System.ComponentModel.DataAnnotations;

namespace ContactManager.Domain.Contracts.Contact.Create;

public record CreateContactRequest
{
    [Required] public string Salutation { get; init; } = null!;
    [Required] public string Firstname { get; init; } = null!;
    [Required] public string Lastname { get; init; } = null!;
    public string? DisplayName { get; init; } = null;
    public DateTime? Birthdate { get; init; }
    [Required] [EmailAddress] public string Email { get; init; } = null!;
    public string? PhoneNumber { get; init; }
}