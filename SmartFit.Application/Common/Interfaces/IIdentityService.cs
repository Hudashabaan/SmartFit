using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> RegisterAsync(string email, string password, string fullName);

        Task<bool> UserExistsAsync(string email);
        Task<string> LoginAsync(string email, string password);
    }
}

