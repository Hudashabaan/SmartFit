using MediatR;
using SmartFit.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Application.Features.Profile.Commands.UpdateProfile
{
    public class UpdateProfileCommand : IRequest<Unit>
    {
        public int Age { get; set; }

        public double Height { get; set; }

        public double Weight { get; set; }

        public Gender Gender { get; set; }

        public ActivityLevel? ActivityLevel { get; set; }

        
    }
}
