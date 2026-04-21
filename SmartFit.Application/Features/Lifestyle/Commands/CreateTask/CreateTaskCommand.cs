using MediatR;
using SmartFit.Application.Features.Lifestyle.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Application.Features.Lifestyle.Commands.CreateTask
{
    public class CreateTaskCommand : IRequest<Guid>
    {
        public CreateTaskDto Dto { get; set; }

        public CreateTaskCommand(CreateTaskDto dto)
        {
            Dto = dto;
        }
    }
}
