using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

namespace SmartFit.Application.Features.Authentication.Commands.register
{

    public record RegisterCommand(
     string Email,
     string Password,
     string ConfirmPassword,
     string FullName
 ) : IRequest<string>;
}

