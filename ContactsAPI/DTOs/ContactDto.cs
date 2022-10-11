namespace ContactsAPI.DTOs
{
    public class ContactDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ImageUrl { get; set; } = "./Assets/user.png";
    }
}
