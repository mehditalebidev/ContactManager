namespace ContactManager.Domain.Contracts.Contact.GetAll;

public record GetContactsResponse
(
    List<Entities.Contact> Contacts
);