using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Application.Features.Lifestyle.Commands.CompleteTask
{
    public class CompleteTaskCommand : IRequest<bool>
    {
        public Guid TaskId { get; set; }
        public Guid UserId { get; set; }
    }
}
