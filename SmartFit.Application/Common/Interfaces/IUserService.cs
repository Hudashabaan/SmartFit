using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Application.Common.Interfaces
{
    public interface IUserService
    {
        Task<string> GetFcmTokenAsync(string userId);
    }
}
