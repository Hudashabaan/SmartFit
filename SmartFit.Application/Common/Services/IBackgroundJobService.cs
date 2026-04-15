using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Application.Common.Services
{
    public interface IBackgroundJobService
    {
        Task ProcessSchedulesAsync();
    }
}
