using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SmartFit.Application.Features.Dashboard.DTOs;

namespace SmartFit.Application.Features.Dashboard.Queries.GetDashboard
{
    public class GetDashboardQuery : IRequest<DashboardDto>
    {
        public string UserId { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}
