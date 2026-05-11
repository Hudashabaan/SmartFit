using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SmartFit.Application.Features.BMI.DTOs;

namespace SmartFit.Application.Features.BMI.Queries.GetBMIHistory
{
    public class GetBMIHistoryQuery
        : IRequest<List<PredictBMIResponseDto>>
    {
    }
}
