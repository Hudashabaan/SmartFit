using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Application.Features.HealthScheduler.DTOs
{
    public class NotificationHistoryDto
    {
        public string Title { get; set; }

        public string Message { get; set; }

        public DateTime SentAt { get; set; }
    }
}
