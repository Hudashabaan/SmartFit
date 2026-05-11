using MediatR;
using SmartFit.Application.Features.BMI.DTOs;

namespace SmartFit.Application.Features.BMI.Commands.PredictBMI
{
    public class PredictBMICommand
        : IRequest<PredictBMIResponseDto>
    {
    }
}