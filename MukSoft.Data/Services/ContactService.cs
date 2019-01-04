using MukSoft.Core.Context;
using MukSoft.Core.Domain;
using MukSoft.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MukSoft.Data.Services
{
    public class ContactService : RepositoryBase<Contact>, IContactService
    {
        public ContactService(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<Contact>> GetAllContactAsync()
        {
            var contacts = await FindAllAsync();
            return contacts.OrderBy(x => x.FirstName);
        }

        public async Task<Contact> GetContactByIdAsync(Guid contactId)
        {
            var contact = await FindByConditionAync(o => o.Id.Equals(contactId));
            return contact.FirstOrDefault();
        }

        public async Task<Guid> CreateContactAsync(Contact contact)
        {
            Create(contact);
            await SaveAsync();
            return (Guid)contact.Id;
        }

        public async Task<bool> UpdateContactAsync(Contact contact)
        {
            Update(contact);
            int rowsAffected = await Save();
            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteContactAsync(Contact contact)
        {
            Delete(contact);
            int rowsAffected = await Save();
            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }
    }
}
