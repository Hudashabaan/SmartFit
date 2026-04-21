using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace SmartFit.Application.Features.Lifestyle.Commands.CompleteTask
{

    public class CompleteTaskValidator : AbstractValidator<CompleteTaskCommand>
    {
        public CompleteTaskValidator()
        {
            RuleFor(x => x.TaskId)
                .NotEmpty().WithMessage("TaskId is required");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required");
        }
    }
}
