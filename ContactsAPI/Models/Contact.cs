using System.ComponentModel.DataAnnotations;

namespace ContactsAPI.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [MaxLength(50)] 
        public string Name { get; set; }

        [MaxLength(200)]
        public string Email { get; set; }

        [StringLength(10)] 
        public string Phone { get; set; }

        [MaxLength(200)]
        public string ImageUrl { get; set; } = "./Assets/user.png";
        public override string ToString()
        {
            return $"{Name} - {Email} - {Phone}";
        }
    }
}
