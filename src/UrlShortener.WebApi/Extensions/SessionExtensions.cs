using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace UrlShortener.WebApi.Extensions
{
    public static class SessionExtensions
    {
        public static void AddValue<T>(this ISession session, string key, T value)
        {
            var existingValues = session.GetValues<T>(key).ToList();
            existingValues.Add(value);
            session.SetString(key, JsonConvert.SerializeObject(existingValues));
        }

        public static IEnumerable<T> GetValues<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? new List<T>() : JsonConvert.DeserializeObject<IEnumerable<T>>(value);
        }
    }
}
