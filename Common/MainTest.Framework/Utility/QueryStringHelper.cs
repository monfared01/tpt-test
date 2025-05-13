using System.Reflection;
using System.Web;

namespace MainTest.Framework.Utility
{
    public static class QueryStringHelper
    {
        public static string ToQueryString<T>(T obj) where T : class
        {
            if (obj == null) return string.Empty;

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                       .Where(p => p.GetValue(obj) != null);

            var queryParameters = new List<string>();

            foreach (var property in properties)
            {
                var value = property.GetValue(obj);
                var encodedValue = HttpUtility.UrlEncode(value.ToString());
                queryParameters.Add($"{property.Name}={encodedValue}");
            }

            return string.Join("&", queryParameters);
        }
    }
}
