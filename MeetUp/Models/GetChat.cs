using System.ComponentModel.DataAnnotations;
namespace MeetUp.Models
{
    public class GetChat
    {
        [Required(ErrorMessage = "* Please Enter something !!!")]
        public string request { get; set; }
    }
    public class GetResponse
    {

        public string request { get; set; }
        public string message { get; set; }
        public string createdAt { get; set; }
    }
}
