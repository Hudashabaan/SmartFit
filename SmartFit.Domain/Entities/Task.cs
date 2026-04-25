using SmartFit.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Domain.Entities
{
    public class Task
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsRecurring { get; set; }

        public DateTime CreatedAt { get; set; }

        public string UserId { get; set; }

        public TaskType Type { get; set; }

        public bool IsCompleted { get; set; } = false; // 🔥 مهم جدًا
    }

}
