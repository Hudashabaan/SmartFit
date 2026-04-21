using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Domain.Entities
{
    public class TrainerInvite
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string TrainerId { get; set; }

        public DateTime ExpiryDate { get; set; }

        public bool IsUsed { get; set; }
    }
}
