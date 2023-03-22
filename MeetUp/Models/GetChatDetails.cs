using System.ComponentModel.DataAnnotations;
namespace MeetUp.Models
{
    public class GetChatDetails
    {
        public string request { get; set; }
        public string message { get; set; }
        public string createdAt { get; set; }
    }
}
