using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Application.Features.Admin.Monitoring.DTOs;
using SmartFit.Domain.Entities;
using SmartFit.Domain.Enums;

namespace SmartFit.Application.Features.Admin.Monitoring.Queries.GetAdminMonitoring
{
    public class GetAdminMonitoringQueryHandler
        : IRequestHandler<GetAdminMonitoringQuery, AdminMonitoringDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public GetAdminMonitoringQueryHandler(
            IApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<AdminMonitoringDto> Handle(
            GetAdminMonitoringQuery request,
            CancellationToken cancellationToken)
        {
            // 🔥 Users
            var totalUsers = await _userManager.Users.CountAsync(cancellationToken);

            // 🔥 Body Images
            var bodyImages = await _context.BodyAnalyses.CountAsync(cancellationToken);

            var approvedBody = await _context.BodyAnalyses
                .CountAsync(x => x.Status == ImageStatus.Approved, cancellationToken);

            var rejectedBody = await _context.BodyAnalyses
                .CountAsync(x => x.Status == ImageStatus.Rejected, cancellationToken);

            var pendingBody = await _context.BodyAnalyses
                .CountAsync(x => x.Status == ImageStatus.Pending, cancellationToken);

            // 🔥 Food Images
            var foodImages = await _context.FoodAnalyses.CountAsync(cancellationToken);

            var approvedFood = await _context.FoodAnalyses
                .CountAsync(x => x.Status == ImageStatus.Approved, cancellationToken);

            var rejectedFood = await _context.FoodAnalyses
                .CountAsync(x => x.Status == ImageStatus.Rejected, cancellationToken);

            var pendingFood = await _context.FoodAnalyses
                .CountAsync(x => x.Status == ImageStatus.Pending, cancellationToken);

            // 🔥 Totals
            var totalImages = bodyImages + foodImages;

            var approvedImages = approvedBody + approvedFood;

            var rejectedImages = rejectedBody + rejectedFood;

            var pendingImages = pendingBody + pendingFood;

            // 🔥 Success Rate
            double successRate = 0;

            if (totalImages > 0)
            {
                successRate = (double)approvedImages / totalImages * 100;
            }

            return new AdminMonitoringDto
            {
                TotalUsers = totalUsers,
                TotalImages = totalImages,
                ApprovedImages = approvedImages,
                RejectedImages = rejectedImages,
                PendingImages = pendingImages,
                SuccessRate = Math.Round(successRate, 2)
            };
        }
    }
}
