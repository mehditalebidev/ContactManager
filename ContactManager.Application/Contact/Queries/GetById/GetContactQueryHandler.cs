using ContactManager.Application.Common.Interfaces.Repositories;
using ContactManager.Application.Contact.Queries.GetContacts;
using ContactManager.Domain.Contracts.Contact.GetAll;
using ContactManager.Domain.Errors;
using ErrorOr;
using MapsterMapper;
using MediatR;

namespace ContactManager.Application.Contact.Queries.GetById
{
    public class GetContactQueryHandler : IRequestHandler<GetContactQuery, ErrorOr<Domain.Entities.Contact>>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;
        public GetContactQueryHandler(IContactRepository contactRepository, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<Domain.Entities.Contact>> Handle(GetContactQuery query, CancellationToken cancellationToken)
        {
            var contact = await  _contactRepository.GetByIdAsync(query.Id);
            if (contact is null)
            {
                return Errors.Contact.NotFound;
            }
            return contact;

        }
    }
}
