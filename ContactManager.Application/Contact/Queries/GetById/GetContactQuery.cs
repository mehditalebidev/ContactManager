using ErrorOr;
using MediatR;

namespace ContactManager.Application.Contact.Queries.GetById
{
    public class GetContactQuery : IRequest<ErrorOr<Domain.Entities.Contact>>
    {
        public int Id { get; set; }
    }

}
