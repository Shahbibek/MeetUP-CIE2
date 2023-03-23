using System.ComponentModel.DataAnnotations;

namespace MeetUp.Models
{
    public class Users
    {

        [Required(ErrorMessage = "* Please Enter First Name")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "* Name must be valid")]
        public string firstName { get; set; }


        [Required(ErrorMessage = "* Please Enter Last Name")]
        public string lastName { get; set; }


        [Required(ErrorMessage = "* Please Enter Email ")]
        [RegularExpression(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "* Please enter valid email")]
        public string email { get; set; }


        [Required(ErrorMessage = "* Please Enter Password")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^.{8,}$", ErrorMessage = "* Password must be 8 digit")]
        public string password { get; set; }


        [Required(ErrorMessage = "* Please Enter Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "* Password must be same")]
        public string confirmPassword { get; set; }
    }
}
