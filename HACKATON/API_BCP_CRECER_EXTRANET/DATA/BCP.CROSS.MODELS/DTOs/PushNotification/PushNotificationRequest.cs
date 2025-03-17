using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.DTOs.PushNotification
{
    public class PushNotificationRequest
    {
        public Target Target { get; set; }
        public int SendType { get; set; }
        public List<Client> Clients { get; set; }
        public List<string> Groups { get; set; }
        public  string Application { get; set; }
        public  string Title { get; set; }
        public  string Message { get; set; }
        public  string Image { get; set; }

        public object Data { get; set; }
        public bool Test { get; set; }
        public string PublicToken { get; set; }
        public string AppUserId { get; set; }
    }

    public class Target
    {
        public bool Push { get; set; }
        public bool Email { get; set; }
        public bool Sms { get; set; }
        public bool WhatsApp { get; set; }
    }

    public class Client
    {
        public string Cic { get; set; }
        public string Idc { get; set; }
        public string PhoneNumber { get; set; }
        public string[] Email { get; set; }
    }

    public class DataOptions
    {
        public string EmailFrom { get; set; }
    }


}
