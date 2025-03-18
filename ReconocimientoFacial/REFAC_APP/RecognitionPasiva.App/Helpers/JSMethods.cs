using Microsoft.JSInterop;

namespace RecognitionPasiva.App.Helpers
{
    public static class JSMethods
    {
        public static ValueTask<object> SetInLocalStorage(this IJSRuntime js, string key, string content)
        { 
            return js.InvokeAsync<object>("localStorage.setItem", key, content);
        }

        public static ValueTask<string> GetFromLocalStorage(this IJSRuntime js, string key)
        { 
            return js.InvokeAsync<string>("localStorage.getItem",key); 
        }

        public static ValueTask<object> RemoveLocalItem(this IJSRuntime js, string key)
        {
            return js.InvokeAsync<object>("localStorage.removeItem", key);
        }

        public static ValueTask<object> RemoveSessionItem(this IJSRuntime js, string key)
        { 
            return js.InvokeAsync<object>("sessionStorage.removeItem", key);
        }
        public static ValueTask<object> SetInSessionStorage(this IJSRuntime js, string key, string content)
        {
            return js.InvokeAsync<object>("sessionStorage.setItem", key, content);
        }

        public static ValueTask<string> GetFromSessionStorage(this IJSRuntime js, string key)
        {
            return js.InvokeAsync<string>("sessionStorage.getItem", key);
        }

        public static ValueTask<string> GetUserAgent(this IJSRuntime js)
        {
            return js.InvokeAsync<string>("getUserAgent");
        }
    }
}
