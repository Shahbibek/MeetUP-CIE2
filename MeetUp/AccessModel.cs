using MeetUp.Models;

namespace MeetUp
{
    public class  AccessModel
    { 
            public IEnumerable<Faculty>  Faculty { get; set; }
            public IEnumerable<Users> Users { get; set; }
    }
}
