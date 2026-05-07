using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SmartFit.Application.Features.Admin.Monitoring.DTOs
{
    public class AdminMonitoringDto
    {
        public int TotalUsers { get; set; }

        public int TotalImages { get; set; }

        public int ApprovedImages { get; set; }

        public int RejectedImages { get; set; }

        public int PendingImages { get; set; }

        public double SuccessRate { get; set; }
    }
}
