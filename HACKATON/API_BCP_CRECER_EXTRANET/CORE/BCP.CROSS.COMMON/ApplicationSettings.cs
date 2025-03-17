using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BCP.CROSS.COMMON
{
    public class ApplicationSettings
    {
        public bool IncludeExceptionStackInResponse { get; set; }
        public ApiPushNotificationSettings ApiPushNotificationSettings { get; set; }
    }

    public class ApiPushNotificationSettings
    {
        public string PushEndpoint { get; set; }
        public string CheckEndpoint { get; set; }
        public string Channel {get; set;}
        public string AppUserId { get; set; }
        public string User {get; set; }
        public string Password {get; set;}
        public string PublicToken {get; set;}
        public string From {get; set;}
        public string CC {get; set; }
        public string Title {get; set;}
        public string Message {get; set; }

    }
}
