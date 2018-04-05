using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace IBM.Api.Weather.CleanedHistoric.Extensions
{
    public static class StringExtensions
    {
        public static string UrlEncode(this string stringToEscape) {
            return Uri.EscapeDataString(stringToEscape)
                .Replace("!", "%21")
                .Replace("*", "%2A")
                .Replace("'", "%27")
                .Replace("(", "%28")
                .Replace(")", "%29");
        }

        public static string UrlDecode(this string stringToUnescape) {
            return UrlDecodeForPost(stringToUnescape)
                .Replace("%21", "!")
                .Replace("%2A", "*")
                .Replace("%27", "'")
                .Replace("%28", "(")
                .Replace("%29", ")");
        }

        public static string UrlDecodeForPost(this string stringToUnescape) {
            stringToUnescape = stringToUnescape.Replace("+", " ");
            return Uri.UnescapeDataString(stringToUnescape);
        }


        public static IEnumerable<KeyValuePair<string, string>> ParseQueryString(string query, bool post = false) {
            var queryParams = query.TrimStart('?').Split('&')
               .Where(x => x != "")
               .Select(x => {
                   var xs = x.Split('=');
                   if (post) {
                       return new KeyValuePair<string, string>(xs[0].UrlDecode(), xs[1].UrlDecodeForPost());
                   } else {
                       return new KeyValuePair<string, string>(xs[0].UrlDecode(), xs[1].UrlDecode());
                   }
               });

            return queryParams;
        }

        public static string ToQueryString(this Dictionary<string, string> query) {
            var queryString = "?";
            queryString += string.Join('&', query.Select(x => x.Key + "=" + x.Value));
            return queryString;
        }

        public static string Wrap(this string input, string wrapper) {
            return wrapper + input + wrapper;
        }

        public static string ToString<T>(this IEnumerable<T> source, string separator) {
            return string.Join(separator, source);
        }
    }
}
