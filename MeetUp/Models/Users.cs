using System.ComponentModel.DataAnnotations;

namespace MeetUp.Models
{
    public class Users
    {
        //public int UserId { get; set; }

        //public int IsDelete { get; set; }


        [Required (ErrorMessage = "* Please Enter First Name")]
        [StringLength(20, MinimumLength =3, ErrorMessage = "* Name must be valid")]
        public string FName { get; set; }


        [Required(ErrorMessage = "* Please Enter Last Name")]
        public string LName { get; set; }



       
        //public string DisplayName { get; set; }


        //[Required(ErrorMessage = "* Please Enter Phone Number")]
        //public string PhoneNo { get; set; }


        
        [Required(ErrorMessage = "* Please Enter Email ")]
        [RegularExpression(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "* Please enter valid email")]
        public string Email { get; set; }



        [Required(ErrorMessage = "* Please Enter Password")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^.{8,}$", ErrorMessage = "* Password must be 8 digit")]
        public string Password { get; set; }



        [Required(ErrorMessage = "* Please Enter Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="* Password must be same")]
        public string CNFPassword { get; set; }


        //public string Role { get; set; }


        //public string CurrentDesignation { get; set; }


        //public string ProfileURL { get; set; }

        

    }
}
