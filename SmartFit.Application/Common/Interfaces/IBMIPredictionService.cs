using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SmartFit.Application.Features.BMI.DTOs;

namespace SmartFit.Application.Common.Interfaces
{
    public interface IBMIPredictionService
    {
        Task<double> PredictBMIAsync(
            PredictBMIRequestDto request);
    }
}
