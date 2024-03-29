﻿using ContactsAPI.Models;

namespace ContactsAPI.DTOs
{
    public class ContactUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ImageUrl { get; set; }
    }
}
