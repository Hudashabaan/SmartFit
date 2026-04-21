using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SmartFit.Domain.Entities
{
    public class TaskLog
    {
        public Guid Id { get; set; }

        public Guid TaskId { get; set; }

        public Guid UserId { get; set; }

        public DateTime Date { get; set; }

        public bool IsCompleted { get; set; }
    }
}
