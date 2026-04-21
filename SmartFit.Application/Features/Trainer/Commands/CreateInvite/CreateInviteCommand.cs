using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SmartFit.Application.Features.Trainer.DTOs;
using MediatR;

namespace SmartFit.Application.Features.Trainer.Commands.CreateInvite
{
   
        public class CreateInviteCommand : IRequest<string>
        {
            public CreateInviteDto Dto { get; set; }
        }
    
}
