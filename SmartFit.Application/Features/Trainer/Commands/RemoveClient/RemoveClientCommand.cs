using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Application.Features.Trainer.Commands.RemoveClient
{
    public class RemoveClientCommand : IRequest<bool>
    {
        public string ClientId { get; set; }
    }
}
