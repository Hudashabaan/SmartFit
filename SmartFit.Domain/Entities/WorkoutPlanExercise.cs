using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SmartFit.Domain.Entities
{
    public class WorkoutPlanExercise
    {
        public Guid WorkoutPlanId { get; set; }

        public WorkoutPlan WorkoutPlan { get; set; } = null!;

        public Guid ExerciseId { get; set; }

        public Exercise Exercise { get; set; } = null!;
    }
}
