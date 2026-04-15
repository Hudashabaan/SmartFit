using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace SmartFit.Application.Features.Profile.Commands.UpdateHeight
{
    public record UpdateHeightCommand(double Height) : IRequest<Unit>;
}
