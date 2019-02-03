using Microsoft.Extensions.Configuration;
using WorkDayLog.Core;

namespace WorkDayLog.Domain.Logs.Repositories
{
    public class LogRepository : RepositoryBase<Log>, ILogRepository
    {
        public LogRepository(IConfiguration config) 
            : base(config, "Logs") { }
    }
}