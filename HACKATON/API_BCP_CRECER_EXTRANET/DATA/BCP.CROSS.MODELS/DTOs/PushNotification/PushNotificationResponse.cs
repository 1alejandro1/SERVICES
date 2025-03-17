using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.DTOs.PushNotification
{
    public class PushNotificationResponse
    {
       public Vias Data { get; set; }
       public string State { get; set; }
       public string Message { get; set; }
    }

    public class Vias
    {
        public Notification Push { get; set; }
        public Notification Email { get; set; }
        public Notification Sms { get; set; }
        public Notification WhatsApp { get; set; }
    }
    
    public class Notification
    {
        public DataNotification Data { get; set; }
        public string State { get; set; }
        public string Message { get; set; }
    }

    public class DataNotification
    {
        public int Success { get; set; }
        public int Failure { get; set; }
        public List<Fail> ClientsFailure { get; set; }
    }
    
    public class Fail
    {
        public string Cic { get; set; }
        public string Idc { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Motive { get; set; }
    }
}
