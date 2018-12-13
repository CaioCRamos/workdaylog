using System;
using System.Collections.Generic;

namespace WorkDayLog.Domain.Logs
{
    public class Log
    {
        private List<Pause> _pauses;
        private List<Activity> _activities;

        public static Log New(Guid userId, DateTime startedAt)
            => new Log
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                StartedAt = startedAt,

                _pauses = new List<Pause>(),
                _activities = new List<Activity>()
            };

        public Guid Id { get; private set; }

        public Guid UserId { get; private set; }

        public DateTime StartedAt { get; private set; }

        public DateTime? EndedAt { get; private set; }

        public IReadOnlyCollection<Pause> Pauses => _pauses;

        public IReadOnlyCollection<Activity> Activities => _activities;

        public void AddPause(string description, DateTime startedAt, DateTime endedAt)
            => _pauses.Add(Pause.New(description, startedAt, endedAt));

        public void AddActivity(string description)
            => _activities.Add(Activity.New(description));
    }
}