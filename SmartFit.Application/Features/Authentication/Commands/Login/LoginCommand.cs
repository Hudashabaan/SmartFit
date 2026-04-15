using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace SmartFit.Application.Features.Authentication.Commands.Login
{
    public record LoginCommand(
        string Email,
        string Password
    ) : IRequest<string>;
}
