using System;

namespace OneStopShop.API.Models.Logging
{
    public partial class Log
    {
        public int logid { get; set; }

        public int eventid { get; set; }

        public int priority { get; set; }

        public string severity { get; set; }

        public string title { get; set; }

        //public DateTime TimeStamp { get; set; }

        public string machinename { get; set; }

        public string appdomainname { get; set; }

        public string processid { get; set; }

        public string processname { get; set; }

        public string threadname { get; set; }

        public string win32threadid { get; set; }

        public string message { get; set; }

        public string formattedmessage { get; set; }
    }
}
