using Microsoft.EntityFrameworkCore;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Application.Common.Services;
using SmartFit.Domain.Entities;
using SmartFit.Domain.Entities.SmartFit.Domain.Entities;
using SmartFit.Domain.Enums;

namespace SmartFit.Infrastructure.Services
{
    public class BackgroundJobService : IBackgroundJobService
    {
        private readonly IApplicationDbContext _context;
        private readonly INotificationService _notification;

        public BackgroundJobService(
            IApplicationDbContext context,
            INotificationService notification)
        {
            _context = context;
            _notification = notification;
        }

        public async Task ProcessSchedulesAsync()
        {
            var now = DateTime.Now;

            var schedules = await _context.HealthSchedules
                .Where(x =>
                    x.IsActive &&
                    !x.IsCompleted &&
                    x.Time <= now &&
                    (x.LastTriggeredAt == null || x.LastTriggeredAt < x.Time))
                .ToListAsync(CancellationToken.None);

            foreach (var schedule in schedules)
            {
                // 🟢 رسالة ديناميك حسب النوع
                var title = GetTitle(schedule.Type);
                var message = schedule.Title;

                // 🥇 ابعت Notification
                await _notification.SendAsync(
                    schedule.UserId,
                    title,
                    message);

                // 🥈 سجل في NotificationHistory 🔥
                _context.NotificationHistories.Add(new NotificationHistory
                {
                    UserId = schedule.UserId,
                    Title = title,
                    Message = message,
                    SentAt = now
                });

                // 🥉 سجل وقت الإرسال
                schedule.LastTriggeredAt = now;

                // 💥 Repeat Logic
                switch (schedule.Repeat)
                {
                    case RepeatType.None:
                        schedule.IsCompleted = true;
                        break;

                    case RepeatType.Daily:
                        schedule.Time = schedule.Time.AddDays(1);
                        break;

                    case RepeatType.Weekly:
                        schedule.Time = schedule.Time.AddDays(7);
                        break;
                }
            }

            await _context.SaveChangesAsync(CancellationToken.None);
        }

        // 🧠 Helper Method للرسائل
        private string GetTitle(ScheduleType type)
        {
            return type switch
            {
                ScheduleType.Water => "💧 Drink Water",
                ScheduleType.Sleep => "😴 Sleep Time",
                ScheduleType.Medication => "💊 Take Medicine",
                _ => "Reminder"
            };
        }
    }
}