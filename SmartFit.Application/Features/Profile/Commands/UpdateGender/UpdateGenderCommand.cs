using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SmartFit.Domain.Enums;

namespace SmartFit.Application.Features.Profile.Commands.UpdateGender
{

    public record UpdateGenderCommand(Gender Gender) : IRequest<Unit>;

}
