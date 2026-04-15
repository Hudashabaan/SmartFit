using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Application.Features.BodyAnalysisFeature.DTOs;

namespace SmartFit.Application.Features.Profile.Queries.GetUserBodyAnalyses
{
    public class GetUserBodyAnalysesQueryHandler
        : IRequestHandler<GetUserBodyAnalysesQuery, List<BodyAnalysisDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public GetUserBodyAnalysesQueryHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<List<BodyAnalysisDto>> Handle(
            GetUserBodyAnalysesQuery request,
            CancellationToken cancellationToken)
        {
            // 🟢 1. جيب اليوزر الحالي
            var userId = _currentUser.UserId;

            if (userId == null)
                throw new Exception("User not authenticated");

            // 🟢 2. هات البيانات
            var result = await _context.BodyAnalyses
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new BodyAnalysisDto
                {
                    Height = x.Height,
                    Weight = x.Weight,
                    Source = x.Source.ToString(),

                    BMI = x.BMI,
                    BodyFatPercentage = x.BodyFatPercentage,
                    FatMass = x.FatMass,
                    MuscleMass = x.MuscleMass,
                    Waist = x.Waist,

                    BodyShape = x.BodyShape.ToString(),
                    Confidence = x.Confidence,

                    ImageUrl = x.ImageUrl,
                    CreatedAt = x.CreatedAt
                })
                .ToListAsync(cancellationToken);

            return result;
        }
    }
}