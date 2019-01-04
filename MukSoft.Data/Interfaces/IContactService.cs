using MukSoft.Core.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MukSoft.Data.Interfaces
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> GetAllContactAsync();
        Task<Contact> GetContactByIdAsync(Guid contactId);
        Task<Guid> CreateContactAsync(Contact contact);
        Task<bool> UpdateContactAsync(Contact contact);
        Task<bool> DeleteContactAsync(Contact contact);
    }
}
