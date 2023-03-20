using MeetUp.Models;
using System.Collections;

namespace MeetUp
{
    public class AccessModel
    {
        public IEnumerable<Faculty> Faculty { get; set; }
        //public IEnumerable<GetChat> GetChat { get; set; }

        public GetChat Chat { get; set; }
    
    }
}
