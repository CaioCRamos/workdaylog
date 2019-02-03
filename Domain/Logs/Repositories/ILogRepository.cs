using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace WorkDayLog.Domain.Logs.Repositories
{
    public interface ILogRepository
    {
        void Add(Log log);

        void Update(Log log);

        Log GetById(Guid logId);

        IEnumerable<Log> GetBy(Expression<Func<Log, bool>> query);
    }
}