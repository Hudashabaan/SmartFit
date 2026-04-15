using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediatR;
using SmartFit.Application.DTOs;

namespace SmartFit.Application.Features.Goals.Queries.GetCurrentGoal
{
    public class GetCurrentGoalQuery : IRequest<GoalDto>
    {
    }
}
