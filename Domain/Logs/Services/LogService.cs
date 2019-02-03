using System;
using System.Collections.Generic;
using System.Linq;
using WorkDayLog.Domain.Logs.Repositories;
using WorkDayLog.Requests;
using WorkDayLog.Responses;

namespace WorkDayLog.Domain.Logs.Services
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;

        public LogService(ILogRepository logRepository)
            => _logRepository = logRepository;

        public IEnumerable<Log> GetAllByUser(Guid userId)
            => _logRepository.GetBy(l => l.UserId.Equals(userId));

        public LogsSummaryResponse GetSummaryByUser(Guid userId, bool showDetails)
        {
            var logs = GetAllByUser(userId);
            var totalExtraMinutes = logs.Sum(l => l.TotalExtraMinutes);

            return new LogsSummaryResponse
            {
                Result = string.Format("{0} hour(s):{1} minute(s)", (int)(totalExtraMinutes / 60), (totalExtraMinutes % 60))
            };
        }

        public LogSubmitResponse Add(Guid userId, LogSubmit newLog)
        {
            if (!newLog.StartedAt.HasValue) 
                return new LogSubmitResponse("The startedAt field must be informed");

            var log = Log.New(userId, newLog.StartedAt.Value);

            return ValidateAndSave(log, newLog, _ => _logRepository.Add(log));
        }

        public LogSubmitResponse Update(Guid logId, LogSubmit editLog)
        {
            var log = _logRepository.GetById(logId);

            if (log == null)
                return new LogSubmitResponse(string.Format("The log Id: {0} wasn't found", logId.ToString()));

            return ValidateAndSave(log, editLog, _ => _logRepository.Update(log));
        }

        private LogSubmitResponse ValidateAndSave(Log log, LogSubmit submit, Action<Log> action)
        {
            var response = new LogSubmitResponse { LogId = log.Id };

            if (submit.EndedAt.HasValue)
                log.SetEndedAt(submit.EndedAt.Value);

            submit.Pauses?.ForEach(
                p => log.AddPause(p.Description, p.StartedAt, p.EndedAt)
            );

            submit.Activities?.ForEach(
                a => log.AddActivity(a.Description)
            );

            //Execute domain validation before sending it to the database
            action.Invoke(log);

            response.WorkedHours = log.WorkedHours;
            response.ExtraHours = log.ExtraHours;
            
            return response;
        }
    }
}