using System;
using System.Collections.Generic;
using WorkDayLog.Requests;
using WorkDayLog.Responses;

namespace WorkDayLog.Domain.Logs.Services
{
    public interface ILogService
    {
        LogSubmitResponse Add(Guid userId, LogSubmit newLog);

        LogSubmitResponse Update(Guid logId, LogSubmit editLog);

        IEnumerable<Log> GetAllByUser(Guid userId);

        LogsSummaryResponse GetSummaryByUser(Guid userId, bool showDetails);
    }
}