using System.Reflection.Metadata.Ecma335;
using ContactManager.Application.Common.Interfaces.Repositories;
using ContactManager.Domain.Errors;
using ErrorOr;
using MapsterMapper;
using MediatR;

namespace ContactManager.Application.Contact.Commands.CreateContact
{
    public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, ErrorOr<Domain.Entities.Contact>>
    {

        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;
        public CreateContactCommandHandler(IContactRepository contactRepository, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<Domain.Entities.Contact>> Handle(CreateContactCommand command, CancellationToken cancellationToken)
        {
            var contact = _mapper.Map<Domain.Entities.Contact>(command);
            var existingContact = await _contactRepository.GetByEmailAsync(contact.Email);
            if (existingContact is not null) //contact already exists
            {
                return Errors.Contact.DuplicateEmail;
            }
            var addedContact = await _contactRepository.Add(contact);
            return addedContact;
            
        }
    }
}
