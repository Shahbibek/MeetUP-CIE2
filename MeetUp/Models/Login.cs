using System.ComponentModel.DataAnnotations;

namespace MeetUp.Models
{
    public class Login
    {
        [Required(ErrorMessage = "* Please Enter Email ")]
        [RegularExpression(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "* Please enter valid email")]
        public string email { get; set; }


        [Required(ErrorMessage = "* Please Enter Password")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^.{8,}$", ErrorMessage = "* Password must be 8 digit")]
        public string password { get; set; }

    }
    public class UserDetails
    {
        public string userId { get; set; }
        public string email { get; set; }
        public string role { get; set; }
        public string token { get; set; }
    }

}
