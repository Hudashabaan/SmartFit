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

            public string Title { get; set; } // مثال: Drink Water

            public string Description { get; set; } // تفاصيل المهمة

            public bool IsRecurring { get; set; } // يومية ولا لأ

            public DateTime CreatedAt { get; set; }

            public Guid UserId { get; set; } // مين صاحب المهمة

             public TaskType Type { get; set; }
    }
    
}
