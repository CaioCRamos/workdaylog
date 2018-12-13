using System;

namespace WorkDayLog.Core
{
    public abstract class EntityBase
    {
        public Guid Id { get; protected set; }
    }
}