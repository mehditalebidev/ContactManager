using ContactManager.Application.Common.Interfaces.Repositories;
using ContactManager.Domain.Errors;
using ErrorOr;
using MapsterMapper;
using MediatR;

namespace ContactManager.Application.Contact.Commands.UpdateContact
{
    public class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand, ErrorOr<Domain.Entities.Contact>>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;
        public UpdateContactCommandHandler(IContactRepository contactRepository, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<Domain.Entities.Contact>> Handle(UpdateContactCommand command, CancellationToken cancellationToken)
        {
            
            var contact = await _contactRepository.GetByIdAsync(command.Id);
            if (contact is null)
            {
                return Errors.Contact.NotFound;
            }
            
            var changedContact = CheckContactChanges(command, contact);

            await _contactRepository.UpdateAsync(changedContact);
            return changedContact;
        }

        private static Domain.Entities.Contact CheckContactChanges(UpdateContactCommand command, Domain.Entities.Contact contact)
        {
            if (command.Salutation is not null)
                contact.Salutation = command.Salutation;
            if (command.Firstname is not null)
                contact.Firstname = command.Firstname;
            if (command.Lastname is not null)
                contact.Lastname = command.Lastname;
            if (command.DisplayName is not null)
                contact.DisplayName = command.DisplayName;
            if (command.Birthdate is not null)
                contact.Birthdate = command.Birthdate;
            if (command.Email is not null)
                contact.Email = command.Email;
            if (command.PhoneNumber is not null)
                contact.PhoneNumber = command.PhoneNumber;
            return contact;
        }
    }
}
