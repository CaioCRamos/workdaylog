using System.Collections.Generic;
using System.Linq;

namespace WorkDayLog.Responses
{
    public abstract class BaseResponse
    {
        public bool Success => Errors == null || !Errors.Any();

        public List<string> Errors { get; set; }

        public BaseResponse() {}

        public BaseResponse(string errorMessage)
            => Errors = new List<string>() { errorMessage };

        public void AddErrorMessage(string errorMessage)
        { 
            Errors = Errors ?? new List<string>();
            Errors.Add(errorMessage);
        }
    }
}