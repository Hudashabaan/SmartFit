using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Application.Common.Interfaces
{
    public interface IAdminLogService
    {
        Task LogAsync(string adminId, string action, string entity, string entityId);
    }
}
