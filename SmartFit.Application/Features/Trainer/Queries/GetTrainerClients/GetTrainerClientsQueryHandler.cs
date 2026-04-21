using System;
using System.Collections.Generic;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Application.Features.Trainer.DTOs;
using SmartFit.Domain.Enums;

namespace SmartFit.Application.Features.Trainer.Queries.GetTrainerClients
{
    public class GetTrainerClientsQueryHandler
        : IRequestHandler<GetTrainerClientsQuery, List<TrainerClientDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public GetTrainerClientsQueryHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<List<TrainerClientDto>> Handle(
            GetTrainerClientsQuery request,
            CancellationToken cancellationToken)
        {
            // 🔥 1. تأكد إن المستخدم Trainer
            if (_currentUser.Role != UserRole.Trainer)
                throw new Exception("Only trainers can view clients");

            var trainerId = _currentUser.UserId;

            // 🔥 2. Query
            var clients = await _context.TrainerClientRelations
                .Where(x => x.TrainerId == trainerId &&
                            x.Status == RelationStatus.Accepted)

                .Include(x => x.Client)
                    .ThenInclude(c => c.Profile)

                .Select(x => new TrainerClientDto
                {
                    ClientId = x.Client.Id,
                    FullName = x.Client.FullName,
                    Age = x.Client.Profile.Age,
                    Weight = x.Client.Profile.Weight,
                    Height = x.Client.Profile.Height,
                    ActivityLevel = x.Client.Profile.ActivityLevel.ToString(),
                    JoinedAt = x.CreatedAt
                })

                .ToListAsync(cancellationToken);

            return clients;
        }
    }
}
