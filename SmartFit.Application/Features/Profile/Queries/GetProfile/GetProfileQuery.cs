using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MediatR;
using SmartFit.Application.Features.Profile.DTOs;

using MediatR;


namespace SmartFit.Application.Features.Profile.Queries.GetProfile
{
    public class GetProfileQuery : IRequest<ProfileDto>
    {
    }
}