using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Application.Features.Trainer.Commands.AddFeedback
{
    public class AddFeedbackCommand : IRequest<bool>
    {
        public string ClientId { get; set; }

        public string Message { get; set; }
    }
}
