using MeetUp.Models;
using System.Collections;
using System.Security.Cryptography.X509Certificates;

namespace MeetUp
{
    public class AccessModel
    {
        public IEnumerable<Faculty> Faculty { get; set; }
        public IEnumerable<GetChatDetails> GetChatDetails { get; set; }

        public GetChat Chat { get; set; }

    }
        public class GetChat
        {
            public string request { get; set; }
        }
}
