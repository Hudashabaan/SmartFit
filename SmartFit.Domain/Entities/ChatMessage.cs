using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Domain.Entities
{
    public class ChatMessage
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public string Message { get; set; }

        public bool IsFromUser { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
