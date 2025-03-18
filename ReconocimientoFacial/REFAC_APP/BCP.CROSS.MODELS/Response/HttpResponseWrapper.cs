using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace BCP.CROSS.MODELS.Response
{
    public class HttpResponseWrapper<T>
    {
        public bool Error { get; set; }
        public T? Response { get; set; }
        public HttpResponseMessage? HttpResponseMessage { get; set; }

        public HttpResponseWrapper(T? response, bool error, HttpResponseMessage httpResponseMessage)
        {
            Error = error;
            Response = response;
            HttpResponseMessage = httpResponseMessage;
        }

        public static HttpResponseWrapper<T> ResponseError(string error)
        {
            HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            response.Content = new StringContent(error);
            return new HttpResponseWrapper<T>(
                default(T),
                true,
                response);
        }
    }
}
