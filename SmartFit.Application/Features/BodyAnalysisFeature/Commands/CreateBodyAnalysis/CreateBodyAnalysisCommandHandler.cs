using MediatR;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Application.Features.BodyAnalysisFeature.Commands.CreateBodyAnalysis;
using SmartFit.Application.Features.BodyAnalysisFeature.DTOs;
using SmartFit.Domain.Entities;
using SmartFit.Domain.Enums;

public class CreateBodyAnalysisCommandHandler
    : IRequestHandler<CreateBodyAnalysisCommand, BodyAnalysisResultDto>
{
    private readonly IAIService _aiService;
    private readonly IApplicationDbContext _context;
    private readonly IFileService _fileService;
    private readonly ICurrentUserService _currentUser;

    public CreateBodyAnalysisCommandHandler(
        IAIService aiService,
        IApplicationDbContext context,
        IFileService fileService,
        ICurrentUserService currentUser)
    {
        _aiService = aiService;
        _context = context;
        _fileService = fileService;
        _currentUser = currentUser;
    }

    public async Task<BodyAnalysisResultDto> Handle(
        CreateBodyAnalysisCommand request,
        CancellationToken cancellationToken)
    {
        if (_currentUser.UserId == null)
            throw new UnauthorizedAccessException("User not authenticated");

        var userId = _currentUser.UserId;

        string? imageUrl = null;
        AnalysisSource source;

        if (request.Image != null)
        {
            imageUrl = await _fileService.SaveImageAsync(request.Image);
        }

        BodyAnalysisResultDto result;

        if (request.Image != null)
        {
            try
            {
                result = await _aiService.AnalyzeImageAsync(request.Image);
                source = AnalysisSource.Image; // ✅ صورة نجحت
            }
            catch
            {
                if (request.Height == null || request.Weight == null)
                    throw new Exception("Manual data required when image fails");

                result = await _aiService.AnalyzeDataAsync(
                    request.Height.Value,
                    request.Weight.Value,
                    request.Age,
                    request.Gender);

                source = AnalysisSource.Manual; // ✅ fallback
            }
        }
        else
        {
            if (request.Height == null || request.Weight == null)
                throw new Exception("Height and Weight required");

            result = await _aiService.AnalyzeDataAsync(
                request.Height.Value,
                request.Weight.Value,
                request.Age,
                request.Gender);

            source = AnalysisSource.Manual; // ✅ manual
        }

        BodyShape bodyShapeEnum;

        if (!Enum.TryParse(result.BodyShape, true, out bodyShapeEnum))
        {
            bodyShapeEnum = BodyShape.Fit;
        }

        var entity = new BodyAnalysis
        {
            Id = Guid.NewGuid(),
            UserId = userId,

            Height = request.Height ?? 0,
            Weight = request.Weight ?? 0,

            BMI = result.BMI,
            BodyFatPercentage = result.BodyFatPercentage,
            FatMass = result.FatMass,
            MuscleMass = result.MuscleMass,
            Waist = result.Waist,

            BodyShape = bodyShapeEnum,
            Confidence = result.Confidence,

            ImageUrl = imageUrl,

            Source = source, // 🔥 أهم سطر

            CreatedAt = DateTime.UtcNow
        };

        _context.BodyAnalyses.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return result;
    }
}