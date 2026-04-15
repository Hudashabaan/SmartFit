using Microsoft.EntityFrameworkCore;
using SmartFit.Domain.Entities;
using SmartFit.Domain.Entities.SmartFit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<UserProfile> UserProfiles { get; }
        DbSet<BodyAnalysis> BodyAnalyses { get; set; }
        DbSet<Meal> Meals { get; set; }
        DbSet<FoodAnalysis> FoodAnalyses { get; }
        DbSet<Goal> Goals { get; }

        DbSet<HealthSchedule> HealthSchedules { get; }

        DbSet<NotificationHistory> NotificationHistories { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
