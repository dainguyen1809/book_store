using Newtonsoft.Json;

namespace Book_Store.Repository
{
    public static class SessionExtension
    {
        public static void SetJson(this ISession session, string key, object values)
        {
            // chuyển values thành chuỗi Json
            session.SetString(key, JsonConvert.SerializeObject(values));
        }

        public static T? GetJson<T>(this ISession session, string key)
        {
            var sessionData = session.GetString(key);
            return sessionData == null ? default(T) : JsonConvert.DeserializeObject<T>(sessionData); // ---> chuyển json thành obj trong c#
        }
    }
}
