using ContactsAPI.DTOs;
using ContactsAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContactsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;

        public ContactsController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        [HttpPost]
        public async Task<ActionResult> AddContact(ContactDto contact)
        {
            await _contactRepository.AddContact(contact);
            return Ok(contact);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<ContactDto>>> GetAllContacts()
        {
            return Ok(await _contactRepository.GetAllContacts());
        }

        [HttpPut("Update")]
        public async Task<ActionResult> UpdateContact(ContactUpdateDto contactUpdate)
        {
            await _contactRepository.UpdateContact(contactUpdate);
            return Ok(contactUpdate);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteContact(int id)
        {
            await _contactRepository.DeleteContact(id);
            return Ok();
        }
    }
}
