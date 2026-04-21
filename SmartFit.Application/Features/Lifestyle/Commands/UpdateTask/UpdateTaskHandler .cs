using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartFit.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Application.Features.Lifestyle.Commands.UpdateTask
{
    public class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public UpdateTaskHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == request.TaskId, cancellationToken);

            if (task == null)
                throw new Exception("Task not found");

            if (task.UserId != request.UserId)
                throw new Exception("Unauthorized");

            task.Title = request.Title;
            task.Description = request.Description;
            task.Type = request.Type;

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
