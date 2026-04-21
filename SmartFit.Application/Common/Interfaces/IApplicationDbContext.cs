using Microsoft.EntityFrameworkCore;
using SmartFit.Domain.Entities;
using SmartFit.Domain.Entities.SmartFit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskEntity = SmartFit.Domain.Entities.Task;

namespace SmartFit.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<ApplicationUser> Users { get; }
        DbSet<UserProfile> UserProfiles { get; }
        DbSet<BodyAnalysis> BodyAnalyses { get; set; }
        DbSet<Meal> Meals { get; set; }
        DbSet<FoodAnalysis> FoodAnalyses { get; }
        DbSet<Goal> Goals { get; }

        DbSet<HealthSchedule> HealthSchedules { get; }

        DbSet<NotificationHistory> NotificationHistories { get; }
        DbSet<TaskEntity> Tasks { get; set; }
        DbSet<TaskLog> TaskLogs { get; set; }
         DbSet<TrainerClientRelation> TrainerClientRelations { get; set; }
         DbSet<TrainerInvite> TrainerInvites { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
