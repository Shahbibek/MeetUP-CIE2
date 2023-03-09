using System.ComponentModel.DataAnnotations;

namespace MeetUp.Models
{
    public class Users
    {
        public int UserId { get; set; }
        public int IsDelete { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string DisplayName { get; set; }
        public string PhoneNo { get; set; }
        
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }
        public string CNFPassword { get; set; }
        public string Role { get; set; }
        public string CurrentDesignation { get; set; }
        public string ProfileURL { get; set; }

    }
}
