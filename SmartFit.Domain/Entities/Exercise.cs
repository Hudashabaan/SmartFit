using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SmartFit.Domain.Entities
{
    public class Exercise
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Level { get; set; } = null!;

        public string FitnessGoal { get; set; } = null!;

        public string FitnessType { get; set; } = null!;

        public bool SupportsHypertension { get; set; }

        public bool SupportsDiabetes { get; set; }

        public string Equipment { get; set; } = null!;

        public int EstimatedCaloriesBurn { get; set; }

        public int DurationInMinutes { get; set; }

        public Guid CategoryId { get; set; }

        public ExerciseCategory Category { get; set; } = null!;

        public ICollection<WorkoutPlanExercise> WorkoutPlanExercises { get; set; }
            = new List<WorkoutPlanExercise>();
    }
}
