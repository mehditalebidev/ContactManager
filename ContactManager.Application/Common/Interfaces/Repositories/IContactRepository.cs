using ContactManager.Domain.Contracts.Contact.GetAll;
using ContactManager.Domain.Entities;

namespace ContactManager.Application.Common.Interfaces.Repositories;

public interface IContactRepository
{
    Task<Domain.Entities.Contact> Add(Domain.Entities.Contact contact);
    public Task<List<Domain.Entities.Contact>> GetAsync(GetContactsFilteredRequest request);
    public Task<Domain.Entities.Contact?> GetByIdAsync(int id);
    public Task<Domain.Entities.Contact?> GetByEmailAsync(string email);
    public Task DeleteByIdAsync(int id);

    Task UpdateAsync(Domain.Entities.Contact contact);
}
