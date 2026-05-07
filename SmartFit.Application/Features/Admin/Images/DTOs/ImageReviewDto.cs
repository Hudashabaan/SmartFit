using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Application.Features.Admin.Images.DTOs
{
    public class ImageReviewDto
    {
        public string Id { get; set; }

        public string Source { get; set; } // Body / Food

        public string ImageUrl { get; set; }

        public string Status { get; set; }
    }
}
