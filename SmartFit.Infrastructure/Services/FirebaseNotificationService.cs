using SmartFit.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebaseAdmin.Messaging;
using MediatR;
using SmartFit.Application.Common.Interfaces;

namespace SmartFit.Infrastructure.Services
{
    public class FirebaseNotificationService : INotificationService
    {
        private readonly IUserService _userService;

        public FirebaseNotificationService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task SendAsync(string userId, string title, string message)
        {
            var token = await _userService.GetFcmTokenAsync(userId);

            if (string.IsNullOrEmpty(token))
                return;

            var msg = new Message()
            {
                Token = token,
                Notification = new Notification
                {
                    Title = title,
                    Body = message
                }
            };

            await FirebaseMessaging.DefaultInstance.SendAsync(msg);
        }
    }
}
