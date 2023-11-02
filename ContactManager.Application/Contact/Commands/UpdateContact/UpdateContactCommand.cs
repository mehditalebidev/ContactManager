using System.ComponentModel.DataAnnotations;
using ErrorOr;
using MediatR;

namespace ContactManager.Application.Contact.Commands.UpdateContact
{
    public record UpdateContactCommand : IRequest<ErrorOr<Domain.Entities.Contact>>
    {
        [Required] public int Id { get; set; }
        public string? Salutation { get; set; }
        public string? Firstname { get; set; } 
        public string? Lastname { get; set; } 
        public string? DisplayName { get; set; }
        public DateTime? Birthdate { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
