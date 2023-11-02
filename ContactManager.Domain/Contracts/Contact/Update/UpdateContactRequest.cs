using System.ComponentModel.DataAnnotations;

namespace ContactManager.Domain.Contracts.Contact.Update;

public class UpdateContactRequest
{
    public string? Salutation { get; set; }
    public string? FirstName { get; set; } 
    public string? LastName { get; set; } 
    public string? DisplayName { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
}