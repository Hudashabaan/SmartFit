using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Application.Common.Interfaces
{
    public interface INotificationService
    {
        Task SendAsync(string userId, string title, string message);
    }
}
