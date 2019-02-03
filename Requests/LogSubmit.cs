using System;
using System.Collections.Generic;

namespace WorkDayLog.Requests
{
    public class LogSubmit
    {
        public DateTime? StartedAt { get; set; }

        public DateTime? EndedAt { get; set; }

        public List<SavePause> Pauses { get; set; }

        public List<SaveActivity> Activities { get; set; }        
    }

    public class SavePause
    {
        public string Description { get; set; }

        public DateTime StartedAt { get; set; }

        public DateTime EndedAt { get; set; }
    }

    public class SaveActivity
    {
        public string Description { get; set; }
    }
}