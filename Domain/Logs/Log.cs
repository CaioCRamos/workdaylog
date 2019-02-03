using System;
using System.Collections.Generic;
using System.Linq;
using WorkDayLog.Core;

namespace WorkDayLog.Domain.Logs
{
    public class Log : EntityBase
    {
        public static Log New(Guid userId, DateTime startedAt)
            => new Log
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                StartedAt = startedAt
            };

        public Guid UserId { get; private set; }

        public DateTime StartedAt { get; private set; }

        public DateTime? EndedAt { get; private set; }

        public List<Pause> Pauses { get; private set; }

        public List<Activity> Activities { get; private set; }

        private double _totalWorkedMinutes
            => EndedAt.HasValue 
                ? ((EndedAt.Value - StartedAt).TotalMinutes - (Pauses?.Sum(p => p.MinutesSpent) ?? 0))
                : 0;

        public double TotalExtraMinutes
            => EndedAt.HasValue
                ? _totalWorkedMinutes - (8 * 60)
                : 0;

        public string WorkedHours
            => EndedAt.HasValue
                ? string.Format("{0}:{1}", (int)(_totalWorkedMinutes / 60), (_totalWorkedMinutes % 60))
                : null;

        public string ExtraHours
            => EndedAt.HasValue
                ? string.Format("{0}:{1}", (int)(TotalExtraMinutes / 60), (TotalExtraMinutes % 60))
                : null;

        public void SetEndedAt(DateTime endedAt) 
            => EndedAt = endedAt;

        public void AddPause(string description, DateTime startedAt, DateTime endedAt)
        {
            Pauses = Pauses ?? new List<Pause>();
            Pauses.Add(Pause.New(description, startedAt, endedAt));
        }

        public void AddActivity(string description)
        {
            Activities = Activities ?? new List<Activity>();
            Activities.Add(Activity.New(description));
        }
    }
}