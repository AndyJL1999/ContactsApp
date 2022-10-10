using AutoMapper;
using ContactsAPI.DTOs;
using ContactsAPI.Models;

namespace ContactsAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ContactDto, Contact>();
            CreateMap<ContactUpdateDto, Contact>();
        }
    }
}
