using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;
using SmartFit.Application.Features.CaloriesPredictions.Commands;

namespace SmartFit.Application.Features.CaloriesPredictions.Commands.CreateCaloriesPrediction
{

        public class CreateCaloriesPredictionCommandValidator
            : AbstractValidator<CreateCaloriesPredictionCommand>
        {
            public CreateCaloriesPredictionCommandValidator()
            {
                RuleFor(x => x.Age)
                    .GreaterThan(0)
                    .LessThan(100);

                RuleFor(x => x.Gender)
                    .NotEmpty();

                RuleFor(x => x.WeightKg)
                    .GreaterThan(0);

                RuleFor(x => x.WorkoutType)
                    .NotEmpty();

                RuleFor(x => x.SessionDurationHours)
                    .GreaterThan(0);

                RuleFor(x => x.AvgBPM)
                    .GreaterThan(0);

                RuleFor(x => x.MaxBPM)
                    .GreaterThan(0);

                RuleFor(x => x.WorkoutFrequencyDaysWeek)
                    .InclusiveBetween(1, 7);

                RuleFor(x => x.BMI)
                    .GreaterThan(0);
            }
        }
    
}
