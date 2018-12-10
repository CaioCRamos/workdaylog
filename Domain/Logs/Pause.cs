using System;

namespace WorkDayLog.Domain.Logs
{
    public class Pause
    {
        public static Pause New(string description, DateTime startedAt, DateTime endedAt)
            => new Pause
            {
                Id = Guid.NewGuid(),
                Description = description,
                StartedAt = startedAt,
                EndedAt = endedAt
            };     

        public Guid Id { get; private set; }

        public string Description { get; private set; }

        public DateTime StartedAt { get; private set; }

        public DateTime EndedAt { get; private set; }
    }
}