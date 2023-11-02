using ContactManager.Application.Common.Interfaces.Repositories;
using ContactManager.Domain.Contracts.Contact.Create;
using ContactManager.Domain.Contracts.Contact.GetAll;
using ErrorOr;
using MapsterMapper;
using MediatR;

namespace ContactManager.Application.Contact.Queries.GetContacts
{
    public class GetContactsQueryHandler : IRequestHandler<GetContactsQuery, ErrorOr<List<Domain.Entities.Contact>>>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;
        public GetContactsQueryHandler(IContactRepository contactRepository, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<List<Domain.Entities.Contact>>> Handle(GetContactsQuery query, CancellationToken cancellationToken)
        {
            var filteredRequest = _mapper.Map<GetContactsFilteredRequest>(query);
            var contacts = await  _contactRepository.GetAsync(filteredRequest);
            return contacts;

        }
    }
}
