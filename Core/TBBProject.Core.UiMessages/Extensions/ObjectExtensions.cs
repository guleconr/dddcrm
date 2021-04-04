using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TBBProject.Core.UiMessages
{
    public static class ObjectExtensions
    {
        public static JsonSerializerSettings JsonSerializerSettings => new JsonSerializerSettings()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore
        };

        public static string ToJson(this object obj)
        {
            return obj != null ? JsonConvert.SerializeObject(obj, JsonSerializerSettings) : null;
        }
    }
}
