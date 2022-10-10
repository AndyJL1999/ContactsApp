using ContactsAPI.DTOs;
using ContactsAPI.Models;

namespace ContactsAPI.Interfaces
{
    public interface IContactRepository
    {
        Task AddContact(ContactDto newContact);
        Task<IEnumerable<Contact>> GetAllContacts();
        Task UpdateContact(ContactUpdateDto contactUpdate);
        Task DeleteContact(int id);
    }
}
