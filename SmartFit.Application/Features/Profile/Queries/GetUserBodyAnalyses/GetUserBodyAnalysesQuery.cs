using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SmartFit.Application.Features.BodyAnalysisFeature.DTOs;

namespace SmartFit.Application.Features.Profile.Queries.GetUserBodyAnalyses
{
    public class GetUserBodyAnalysesQuery : IRequest<List<BodyAnalysisDto>>
    {
    }
}
