using MediatR;
using SmartFit.Application.Features.Trainer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Application.Features.Trainer.Queries.GetWeeklyProgress
{
    public class GetWeeklyProgressQuery : IRequest<WeeklyProgressDto>
    {
        public string ClientId { get; set; }
    }
}
