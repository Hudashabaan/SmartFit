using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SmartFit.Application.DTOs;

namespace SmartFit.Application.Features.Profile.Queries.GetProfile
{

    public record GetProfileQuery : IRequest<ProfileDto>;
}