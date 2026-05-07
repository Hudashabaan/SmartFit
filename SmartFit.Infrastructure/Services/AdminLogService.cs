using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SmartFit.Application.Common.Interfaces;
using SmartFit.Domain.Entities;

namespace SmartFit.Infrastructure.Services
{

    public class AdminLogService : IAdminLogService
    {
        private readonly IApplicationDbContext _context;

        public AdminLogService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async System.Threading.Tasks.Task LogAsync(string adminId, string action, string entity, string entityId)
        {
            var log = new AdminLog
            {
                AdminId = adminId,
                Action = action,
                Entity = entity,
                EntityId = entityId
            };

            _context.AdminLogs.Add(log);
            await _context.SaveChangesAsync(CancellationToken.None);
        }

        System.Threading.Tasks.Task IAdminLogService.LogAsync(string adminId, string action, string entity, string entityId)
        {
            throw new NotImplementedException();
        }
    }
}
