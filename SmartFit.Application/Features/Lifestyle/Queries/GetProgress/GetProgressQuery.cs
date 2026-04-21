using MediatR;
using SmartFit.Application.Features.Lifestyle.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Application.Features.Lifestyle.Queries.GetProgress
{
    public class GetProgressQuery : IRequest<ProgressDto>
    {
        public Guid UserId { get; set; }
    
    }
}
