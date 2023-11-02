using System.ComponentModel.DataAnnotations;
using ContactManager.Domain.Contracts.Contact.Create;
using ContactManager.Domain.Contracts.Contact.GetAll;
using ErrorOr;
using MediatR;

namespace ContactManager.Application.Contact.Queries.GetContacts
{
    public class GetContactsQuery : IRequest<ErrorOr<List<Domain.Entities.Contact>>>
    {
        public string? Salutation { get; init; } = null;
        public string? Firstname { get; init; } = null;
        public string? Lastname { get; init; } = null;
        public string? DisplayName { get; init; } = null;
        public string? Email { get; init; } = null;
        public string? PhoneNumber { get; init; } = null;
        public bool? HastBirthdaySoon { get; init; } = null;
        public int? Page { get; init; } = 1;
        public int? PageSize { get; init; } = 10;

    }

}
