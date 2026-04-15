using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Application.Common.Interfaces
{
    public interface IImageService
    {
        Task<string> UploadImageAsync(Stream fileStream, string fileName);
    }
}
