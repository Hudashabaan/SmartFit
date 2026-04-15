using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Domain.Entities;

namespace SmartFit.Infrastructure.Services
{
  

    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> GetFcmTokenAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            return user?.FcmToken;
        }
    }
}
