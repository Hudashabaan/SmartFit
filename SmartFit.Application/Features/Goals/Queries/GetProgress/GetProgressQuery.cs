using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace SmartFit.Application.Features.Goals.Queries.GetProgress
{
    public class GetProgressQuery : IRequest<double>
    {
    }
}
