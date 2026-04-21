using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Application.Features.Trainer.DTOs
{
        public class TrainerClientDto
        {
            public string ClientId { get; set; }

            public string FullName { get; set; }

            public int Age { get; set; }

            public double Weight { get; set; }

            public double Height { get; set; }

            public string ActivityLevel { get; set; }

            public DateTime JoinedAt { get; set; }
        }
    
}
