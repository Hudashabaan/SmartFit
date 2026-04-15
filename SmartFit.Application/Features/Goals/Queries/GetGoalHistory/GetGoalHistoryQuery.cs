using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SmartFit.Application.DTOs;
using System.Collections.Generic;

namespace SmartFit.Application.Features.Goals.Queries.GetGoalHistory
{
    public class GetGoalHistoryQuery : IRequest<List<GoalDto>>
    {
    }
}
