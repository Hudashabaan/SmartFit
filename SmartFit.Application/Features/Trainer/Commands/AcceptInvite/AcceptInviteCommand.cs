using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartFit.Application.Features.Trainer.DTOs;
using MediatR;

namespace SmartFit.Application.Features.Trainer.Commands.AcceptInvite
{ 
        public class AcceptInviteCommand : IRequest<string>
        {
            public AcceptInviteDto Dto { get; set; }
        }
    
}
