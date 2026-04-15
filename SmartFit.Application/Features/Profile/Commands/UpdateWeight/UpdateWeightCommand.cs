using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

namespace SmartFit.Application.Features.Profile.Commands.UpdateWeight
{
    public record UpdateWeightCommand(double Weight) : IRequest<Unit>;
}
