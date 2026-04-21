using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;
using System.Collections.Generic;
using SmartFit.Application.Features.Trainer.DTOs;

namespace SmartFit.Application.Features.Trainer.Queries.GetTrainerClients
{
        public class GetTrainerClientsQuery : IRequest<List<TrainerClientDto>>
        {
        }
    
}
