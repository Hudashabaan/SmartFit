using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Domain.Entities
{
    public class ExerciseCategory
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public ICollection<Exercise> Exercises { get; set; }
            = new List<Exercise>();
    }
}
