using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Application.Features.Trainer.DTOs
{
    public class FeedbackDto
    {
        public Guid Id { get; set; }

        public string TrainerName { get; set; }

        public string Message { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
