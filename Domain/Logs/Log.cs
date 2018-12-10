using System;
using System.Collections.Generic;

namespace WorkDayLog.Domain.Logs
{
    public class Log
    {
        private List<Pause> _pauses;

        public static Log New(DateTime startedAt)
            => new Log
            {
                Id = Guid.NewGuid(),
                StartedAt = startedAt
            };

        public Guid Id { get; private set; }

        public DateTime StartedAt { get; private set; }

        public DateTime? EndedAt { get; private set; }

        public IReadOnlyCollection<Pause> Pauses => _pauses;

        public void AddPause(string description, DateTime startedAt, DateTime endedAt)
        {
            _pauses = _pauses ?? new List<Pause>();

            _pauses.Add(Pause.New(description, startedAt, endedAt));
        }
    }
}