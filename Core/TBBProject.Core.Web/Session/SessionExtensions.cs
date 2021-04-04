using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TBBProject.Core.Common;

namespace TBBProject.Core.Web
{
    public class SessionExtensionMarker
    {
    }
    public static class SessionExtensions
    {

        public static void SetObject(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }

        public static async Task<byte[]> GetAsync(this ISession session, string key)
        {
            if (IsValid(key))
            {
                await session.LoadAsync();
                return session.Get(key);
            }
            else
            {
                return null;
            }
        }
        public static async Task<int?> GetInt32Async(this ISession session, string key)
        {
            if (IsValid(key))
            {
                await session.LoadAsync();
                return session.GetInt32(key);
            }
            else
            {
                return 0;
            }
        }
        public static async Task<string> GetStringAsync(this ISession session, string key)
        {
            if (IsValid(key))
            {
                await session.LoadAsync();
                return session.GetString(key);
            }
            else
            {
                return "";
            }

        }
        public static async Task SetInt32Async(this ISession session, string key, int value)
        {
            if (IsValid(key, value))
            {
                await session.LoadAsync();
                session.SetInt32(key, value);
            }
        }
        public static async Task SetStringAsync(this ISession session, string key, string value)
        {
            if (IsValid(key, value))
            {
                await session.LoadAsync();
                session.SetString(key, value);
            }
        }


        public static void SetToAppScope(this ISession session, string key, byte[] value)
        {
            if (IsValid(key, value))
            {
                var appName = Assembly.GetCallingAssembly().GetName().Name;
                session.Set(appName + key, value);
            }
        }

        public static byte[] GetFromAppScope(this ISession session, string key)
        {
            var appName = Assembly.GetCallingAssembly().GetName().Name;
            return session.Get(appName + key);
        }

        public static async Task SetToAppScopeAsync(this ISession session, string key, byte[] value)
        {
            if (IsValid(key, value))
            {
                await session.LoadAsync();
                session.SetToAppScope(key, value);
            }
        }

        public static async Task<byte[]> GetFromAppScopeAsync(this ISession session, string key)
        {
            await session.LoadAsync();
            return session.GetFromAppScope(key);
        }

        public static async Task SetObjectAsync(this ISession session, string key, object value)
        {
            if (IsValid(key, value))
            {
                await session.SetStringAsync(key, JsonConvert.SerializeObject(value));
            }
        }

        public static async Task<T> GetObjectAsync<T>(this ISession session, string key)
        {
            var value = await session.GetStringAsync(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value,
                new JsonSerializerSettings
                {
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                });
        }
        private static bool IsValid(string key, object value)
        {
            var logger = TBBProjectContext.Current.Resolve<ILogger<SessionExtensionMarker>>();

            if (string.IsNullOrEmpty(key) && value != null)
            {
                logger?.LogError("Key and value cannot be null! Session value not set.");
                return false;
            }
            if (string.IsNullOrEmpty(key) == false && value == null)
            {
                logger?.LogError("Key is not null, Value is null ", new object[] { key });
                return true;
            }
            else
            {
                return true;
            }
        }
        private static bool IsValid(string key)
        {
            var logger = TBBProjectContext.Current.Resolve<ILogger<SessionExtensionMarker>>();

            if (string.IsNullOrEmpty(key))
            {
                logger?.LogError("Key cannot be null! Session value cannot get its value.");
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}