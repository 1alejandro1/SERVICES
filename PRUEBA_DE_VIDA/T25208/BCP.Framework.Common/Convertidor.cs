using System.Text;

namespace BCP.Framework.Common
{
    public static class Convertidor
    {
        public static string ToUTF8(this string text)
        {
            return Encoding.UTF8.GetString(Encoding.Default.GetBytes(text));
        }
    }
}
