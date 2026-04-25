using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Domain.Entities
{
    public class Feedback
    {
        public Guid Id { get; set; }

        public string TrainerId { get; set; }  // 🔥 string (Identity)

        public string ClientId { get; set; }   // 🔥 string

        public string Message { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
