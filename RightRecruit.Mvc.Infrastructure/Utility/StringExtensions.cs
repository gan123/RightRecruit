using System.Text;

namespace RightRecruit.Mvc.Infrastructure.Utility
{
    public static class StringExtensions
    {
        public static byte[] ToByteArray(this string source)
        {
            return Encoding.UTF8.GetBytes(source);
        }

        public static string ToPlainString(this byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }
    }
}