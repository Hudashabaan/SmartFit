using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Domain.Entities
{
    using System;

    namespace SmartFit.Domain.Entities
    {
        public class NotificationHistory
        {
            public Guid Id { get; set; }

            public string UserId { get; set; }

            public string Title { get; set; }

            public string Message { get; set; }

            public DateTime SentAt { get; set; }
        }
    }
}
