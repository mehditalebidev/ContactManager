using System.ComponentModel.DataAnnotations;
using ErrorOr;
using MediatR;

namespace ContactManager.Application.Contact.Commands.CreateContact
{
    public record CreateContactCommand : IRequest<ErrorOr<Domain.Entities.Contact>>
    {
        [Required] public string Salutation { get; init; } = null!;
        [Required] public string Firstname { get; init; } = null!;
        [Required] public string Lastname { get; init; } = null!;
        public string? DisplayName { get; init; } = null;
        public DateTime? Birthdate { get; init; }
        [Required] [EmailAddress] public string Email { get; init; } = null!;
        public string? PhoneNumber { get; init; }

    }
}
