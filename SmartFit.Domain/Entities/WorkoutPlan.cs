using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SmartFit.Domain.Entities
{
    public class WorkoutPlan
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public string Goal { get; set; } = null!;

        public string Level { get; set; } = null!;

        public int TotalDuration { get; set; }

        public ICollection<WorkoutPlanExercise> WorkoutPlanExercises { get; set; }
            = new List<WorkoutPlanExercise>();
    }
}
