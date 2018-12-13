using System;

namespace WorkDayLog.Domain.Logs
{
    public class Activity
    {
        public static Activity New(string description)
            => new Activity
            {
                Id = Guid.NewGuid(),
                Description = description
            };

        public Guid Id { get; private set; }

        public string Description { get; private set; }
    }
}