using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediatR;
using SmartFit.Application.Features.Admin.Images.DTOs;

namespace SmartFit.Application.Features.Admin.Images.Queries.GetAllImages
{
    public class GetAllImagesQuery : IRequest<List<ImageReviewDto>>
    {
    }
}