using ContactManager.Application.Common.Interfaces.Repositories;
using ContactManager.Domain.Contracts.Contact.Delete;
using ContactManager.Domain.Errors;
using ErrorOr;
using MapsterMapper;
using MediatR;

namespace ContactManager.Application.Contact.Commands.DeleteContact
{
    public class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand, ErrorOr<DeleteContactResponse>>
    {

        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;
        public DeleteContactCommandHandler(IContactRepository contactRepository, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<DeleteContactResponse>> Handle(DeleteContactCommand command, CancellationToken cancellationToken)
        {
            
            var contact = await _contactRepository.GetByIdAsync(command.Id);
            if (contact is null)
            {
                return Errors.Contact.NotFound;
            }
            await _contactRepository.DeleteByIdAsync(command.Id);
            return new DeleteContactResponse(true);
       
            
        }
    }
}
