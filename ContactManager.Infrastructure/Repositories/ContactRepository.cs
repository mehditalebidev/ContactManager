using ContactManager.Application.Common.Interfaces.Repositories;
using ContactManager.Application.Common.Interfaces.Services;
using ContactManager.Domain.Contracts.Contact.GetAll;
using ContactManager.Domain.Entities;
using ContactManager.Infrastructure.Context;
using ContactManager.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Infrastructure.Repositories
{
    public class ContactRepository : IContactRepository
    {

        private readonly ContactManagerDbContext _context;
        private readonly IDateTimeProvider _dateTimeProvider;
        
        public ContactRepository(ContactManagerDbContext context, IDateTimeProvider dateTimeProvider)
        {
            _context = context;
            _dateTimeProvider = dateTimeProvider;
        }
        public async Task<Contact> Add(Contact contact)
        {
            var contactAdded = await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();
            return contactAdded.Entity;
        }

        public async Task<List<Contact>> GetAsync(GetContactsFilteredRequest request)
        {
            var query = CreateQuery(request);

            var contacts = await query.ToListAsync();
            
            return contacts;
        }

        private IQueryable<Contact> CreateQuery(GetContactsFilteredRequest request)
        {
            var query = _context.Contacts.AsQueryable();

            query = CheckSalutation(request, query);
            query = CheckFirstName(request, query);
            query = CheckLastName(request, query);
            query = CheckDisplayName(request, query);
            query = CheckEmail(request, query);
            query = CheckPhoneNumber(request, query);
            query = CheckHasBirthdateSoon(request, query);
            query = CheckPagination(request, query);

            return query;
        }

        private static IQueryable<Contact> CheckPagination(GetContactsFilteredRequest request, IQueryable<Contact> query)
        {
            if (request.Page.HasValue && request.PageSize.HasValue)
            {
                int skip = (request.Page.Value - 1) * request.PageSize.Value;
                query = query.Skip(skip).Take(request.PageSize.Value);
            }

            return query;
        }

        private static IQueryable<Contact> CheckHasBirthdateSoon(GetContactsFilteredRequest request, IQueryable<Contact> query)
        {
            if (request.HastBirthdaySoon.HasValue && request.HastBirthdaySoon.Value)
            {
                var today = DateTime.UtcNow.Date;
                var soon = today.AddDays(14);
                query = query.Where(contact => contact.Birthdate.HasValue &&
                                               new DateTime(today.Year, contact.Birthdate.Value.Month,
                                                   contact.Birthdate.Value.Day) >= today &&
                                               new DateTime(today.Year, contact.Birthdate.Value.Month,
                                                   contact.Birthdate.Value.Day) <= soon);
            }

            return query;
        }

        private static IQueryable<Contact> CheckPhoneNumber(GetContactsFilteredRequest request, IQueryable<Contact> query)
        {
            if (!string.IsNullOrWhiteSpace(request.PhoneNumber))
                query = query.Where(contact => contact.PhoneNumber.Contains(request.PhoneNumber));
            return query;
        }

        private static IQueryable<Contact> CheckEmail(GetContactsFilteredRequest request, IQueryable<Contact> query)
        {
            if (!string.IsNullOrWhiteSpace(request.Email))
                query = query.Where(contact => contact.Email.Contains(request.Email));
            return query;
        }

        private static IQueryable<Contact> CheckDisplayName(GetContactsFilteredRequest request, IQueryable<Contact> query)
        {
            if (!string.IsNullOrWhiteSpace(request.DisplayName))
                query = query.Where(contact => contact.DisplayName.Contains(request.DisplayName));
            return query;
        }

        private static IQueryable<Contact> CheckLastName(GetContactsFilteredRequest request, IQueryable<Contact> query)
        {
            if (!string.IsNullOrWhiteSpace(request.Lastname))
                query = query.Where(contact => contact.Lastname.Contains(request.Lastname));
            return query;
        }

        private static IQueryable<Contact> CheckFirstName(GetContactsFilteredRequest request, IQueryable<Contact> query)
        {
            if (!string.IsNullOrWhiteSpace(request.Firstname))
                query = query.Where(contact => contact.Firstname.Contains(request.Firstname));
            return query;
        }

        private static IQueryable<Contact> CheckSalutation(GetContactsFilteredRequest request, IQueryable<Contact> query)
        {
            if (!string.IsNullOrWhiteSpace(request.Salutation))
                query = query.Where(contact => contact.Salutation.Contains(request.Salutation));
            return query;
        }


        public async Task<Contact?> GetByIdAsync(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            return contact;
        }

        public Task<Contact?> GetByEmailAsync(string email)
        {
            var contact = _context.Contacts.FirstOrDefaultAsync(contact => contact.Email == email);
            return contact;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            var removed = _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Contact contact)
        {
            contact.LastChangeTimestamp = _dateTimeProvider.UtcNow;
            _context.Contacts.Update(contact);
            await _context.SaveChangesAsync();
        }
    }
}
