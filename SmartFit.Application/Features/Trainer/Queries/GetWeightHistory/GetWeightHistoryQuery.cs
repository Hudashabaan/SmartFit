using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SmartFit.Application.Features.Trainer.DTOs;

namespace SmartFit.Application.Features.Trainer.Queries.GetWeightHistory
{
    public class GetWeightHistoryQuery : IRequest<List<WeightHistoryDto>>
    {
        public string ClientId { get; set; }
    }
}
