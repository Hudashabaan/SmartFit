using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SmartFit.Domain.Entities
{
    public class AdminLog
    {
        public int Id { get; set; }

        public string AdminId { get; set; }

        public string Action { get; set; }

        public string Entity { get; set; } // User / Image

        public string EntityId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
