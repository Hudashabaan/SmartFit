using MediatR;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Application.DTOs;
using SmartFit.Domain.Entities;

namespace SmartFit.Application.Features.FoodAnalysisFeature.Commands.AnalyzeFood
{
    public class AnalyzeFoodCommandHandler
        : IRequestHandler<AnalyzeFoodCommand, FoodAnalysisDto>
    {
        private readonly IFoodRecognitionService _foodService;
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly IImageService _imageService;

        public AnalyzeFoodCommandHandler(
            IFoodRecognitionService foodService,
            IApplicationDbContext context,
            ICurrentUserService currentUser,
            IImageService imageService)
        {
            _foodService = foodService;
            _context = context;
            _currentUser = currentUser;
            _imageService = imageService;
        }

        public async Task<FoodAnalysisDto> Handle(
            AnalyzeFoodCommand request,
            CancellationToken cancellationToken)
        {
            // 🔥 1. نجيب اليوزر
            var userId = _currentUser.UserId
                ?? throw new Exception("User not authenticated");

            // 🔥 2. نحول الصورة لـ Stream
            using var stream = request.Image.OpenReadStream();

            // 🔥 3. نرفع الصورة على Cloudinary
            var imageUrl = await _imageService.UploadImageAsync(
                stream,
                request.Image.FileName
            );

            // 🔥 4. نبعت للـ AI (Fake حالياً)
            var result = await _foodService.AnalyzeAsync(stream);

            // 🔥 5. نحفظ في DB
            var entity = new FoodAnalysis
            {
                Id = Guid.NewGuid(),
                ImageUrl = imageUrl, // ✔️ بقى حقيقي
                FoodName = result.FoodName,
                Calories = result.Calories,
                Protein = result.Protein,
                Carbs = result.Carbs,
                Fat = result.Fat,
                Confidence = result.Confidence,
                CreatedAt = DateTime.UtcNow,
                UserId = userId
            };

            _context.FoodAnalyses.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            // 🔥 6. نرجع النتيجة
            return result;
        }
    }
}
