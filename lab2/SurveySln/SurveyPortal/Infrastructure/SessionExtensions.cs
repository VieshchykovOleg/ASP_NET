using System.Text.Json;

namespace SurveyPortal.Infrastructure
{
    public static class SessionExtensions
    {
        // Метод для збереження об'єкта в сесії
        public static void SetJson(this ISession session, string key, object value)
        { 
       
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        // Метод для отримання об'єкта з сесії
        public static T? GetJson<T>(this ISession session, string key)
        {
            var sessionData = session.GetString(key);
            return sessionData == null
                ? default(T)
                : JsonSerializer.Deserialize<T>(sessionData);
        }
    }
}
