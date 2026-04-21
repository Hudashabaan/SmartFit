using SmartFit.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Domain.Entities
{
    public class TrainerClientRelation
    {
        public Guid Id { get; set; }

        public string TrainerId { get; set; }
        public ApplicationUser Trainer { get; set; }

        public string ClientId { get; set; }
        public ApplicationUser Client { get; set; }

        public RelationStatus Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
