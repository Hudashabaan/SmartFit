using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SmartFit.Application.Features.Admin.Monitoring.DTOs;

namespace SmartFit.Application.Features.Admin.Monitoring.Queries.GetAdminMonitoring
{
    public class GetAdminMonitoringQuery
        : IRequest<AdminMonitoringDto>
    {
    }
}
