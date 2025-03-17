using System;

namespace OCR.Application.Api.Utils
{
    public class OperationUtil
    {
        public static string GetOperation()
        {
            return DateTime.Now.ToString("ddMMyyyyHHmmss");
        }
    }
}
