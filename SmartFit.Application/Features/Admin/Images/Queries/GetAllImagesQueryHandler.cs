using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Application.Features.Admin.Images.DTOs;

namespace SmartFit.Application.Features.Admin.Images.Queries.GetAllImages
{
    public class GetAllImagesQueryHandler
        : IRequestHandler<GetAllImagesQuery, List<ImageReviewDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllImagesQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ImageReviewDto>> Handle(
      GetAllImagesQuery request,
      CancellationToken cancellationToken)
        {
            var bodyImages = _context.BodyAnalyses
                .Select(x => new ImageReviewDto
                {
                    Id = x.Id.ToString(),
                    Source = "Body",
                    ImageUrl = x.ImageUrl,
                    Status = x.Status.ToString()
                });

            var foodImages = _context.FoodAnalyses
                .Select(x => new ImageReviewDto
                {
                    Id = x.Id.ToString(),
                    Source = "Food",
                    ImageUrl = x.ImageUrl,
                    Status = x.Status.ToString()
                });

            return await bodyImages
                .Concat(foodImages)
                .ToListAsync(cancellationToken);
        }
    }
    
}
