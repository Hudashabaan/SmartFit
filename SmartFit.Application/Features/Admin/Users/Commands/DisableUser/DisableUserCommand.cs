using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace SmartFit.Application.Features.Admin.Users.Commands.DisableUser
{
    public class DisableUserCommand : IRequest<bool>
    {
        public string UserId { get; set; }
    }
}
