using MediatR;
using SmartFit.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Application.Features.Lifestyle.Commands.UpdateTask
{
    public class UpdateTaskCommand : IRequest<bool>
    {
        public Guid TaskId { get; set; }
        public Guid UserId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public TaskType Type { get; set; }
    }
}
