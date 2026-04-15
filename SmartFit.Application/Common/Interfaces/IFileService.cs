using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Application.Common.Interfaces
{
    public interface IFileService
    {
        Task<string> SaveImageAsync(IFormFile file);
    }
}
