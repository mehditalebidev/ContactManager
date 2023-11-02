using System.ComponentModel.DataAnnotations;
using ContactManager.Domain.Contracts.Contact.Delete;
using ErrorOr;
using MediatR;

namespace ContactManager.Application.Contact.Commands.DeleteContact
{
    public record DeleteContactCommand : IRequest<ErrorOr<DeleteContactResponse>>
    {
        [Required] public int Id { get; set; }
    }
}
