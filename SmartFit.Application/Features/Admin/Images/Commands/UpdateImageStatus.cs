using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SmartFit.Domain.Enums;

namespace SmartFit.Application.Features.Admin.Images.Commands.UpdateImageStatus
{
    public class UpdateImageStatusCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public string Source { get; set; } // Body / Food

        public ImageStatus Status { get; set; }
    }
}
