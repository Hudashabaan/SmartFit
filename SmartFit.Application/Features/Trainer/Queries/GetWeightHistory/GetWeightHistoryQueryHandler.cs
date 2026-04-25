using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Application.Features.Trainer.DTOs;
using SmartFit.Domain.Enums;

namespace SmartFit.Application.Features.Trainer.Queries.GetWeightHistory

{ 
    public class GetWeightHistoryQueryHandler
        : IRequestHandler<GetWeightHistoryQuery, List<WeightHistoryDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public GetWeightHistoryQueryHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<List<WeightHistoryDto>> Handle(
            GetWeightHistoryQuery request,
            CancellationToken cancellationToken)
        {
            // 🔥 1. Check Trainer
            if (_currentUser.Role != UserRole.Trainer)
                throw new Exception("Only trainers can view weight history");

            // 🔥 2. Check relation
            var relation = await _context.TrainerClientRelations
                .AnyAsync(x =>
                    x.TrainerId == _currentUser.UserId &&
                    x.ClientId == request.ClientId &&
                    x.Status == RelationStatus.Accepted,
                    cancellationToken);

            if (!relation)
                throw new Exception("Unauthorized");

            // 🔥 3. Get Weight History
            var history = await _context.BodyAnalyses
                .Where(x => x.UserId == request.ClientId)
                .OrderBy(x => x.CreatedAt)
                .Select(x => new WeightHistoryDto
                {
                    Date = x.CreatedAt,
                    Weight = x.Weight
                })
                .ToListAsync(cancellationToken);

            return history;
        }
    }
}
