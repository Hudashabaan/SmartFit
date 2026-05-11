using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SmartFit.Domain.Enums;

namespace SmartFit.Application.Features.Profile.Commands.CreateProfile
{
    public class CreateProfileCommand : IRequest<string>
    {
        public string FullName { get; set; } = string.Empty;

        public int Age { get; set; }

        public float Height { get; set; }

        public float Weight { get; set; }

        public Gender Gender { get; set; }

        public bool HasHypertension { get; set; }

        public bool HasDiabetes { get; set; }

        public FitnessGoal FitnessGoal { get; set; }

        public FitnessType FitnessType { get; set; }

        public string? ProfilePictureUrl { get; set; }
    }
}
