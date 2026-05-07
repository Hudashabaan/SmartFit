using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartFit.Domain.Entities;

namespace SmartFit.Application.Features.Admin.Export.Queries.ExportUsersCsv
{
    public class ExportUsersCsvQueryHandler
        : IRequestHandler<ExportUsersCsvQuery, byte[]>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ExportUsersCsvQueryHandler(
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<byte[]> Handle(
            ExportUsersCsvQuery request,
            CancellationToken cancellationToken)
        {
            var users = await _userManager.Users.ToListAsync(cancellationToken);

            var csv = new StringBuilder();

            // 🔥 Header
            csv.AppendLine("Id,Email,UserName,IsActive");

            // 🔥 Rows
            foreach (var user in users)
            {
                csv.AppendLine(
                    $"{user.Id}," +
                    $"{user.Email}," +
                    $"{user.UserName}," +
                    $"{user.IsActive}");
            }

            return Encoding.UTF8.GetBytes(csv.ToString());
        }
    }
}
