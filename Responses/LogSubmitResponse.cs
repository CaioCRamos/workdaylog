using System;

namespace WorkDayLog.Responses
{
    public class LogSubmitResponse : BaseResponse
    {
        public LogSubmitResponse() {}

        public LogSubmitResponse(string errorMessage) 
            : base(errorMessage) { }

        public Guid LogId { get; set; }

        public string WorkedHours { get; set; }

        public string ExtraHours { get; set; }
    }
}