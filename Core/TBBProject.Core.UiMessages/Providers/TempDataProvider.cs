using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace TBBProject.Core.UiMessages
{
    public class TempDataProvider : ITempDataProvider
    {
        private readonly HttpContext _context;
        private readonly JsonSerializerSettings _serializerSettings;
        private readonly ITempDataDictionaryFactory _tempDataDictionaryFactory;

        private ITempDataDictionary TempData => _tempDataDictionaryFactory?.GetTempData(_context);

        public TempDataProvider(ITempDataDictionaryFactory tempDataDictionaryFactory, IHttpContextAccessor httpContextAccessor)
        {
            _tempDataDictionaryFactory = tempDataDictionaryFactory;
            _context = httpContextAccessor.HttpContext;
            _serializerSettings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto
            };
        }

        public T Get<T>(string key) where T : class
        {
            if (TempData.ContainsKey(key))
            {
                var data = TempData[key] as string;
                var obj = JsonConvert.DeserializeObject<T>(data);
                return obj;
            }
            return default;
        }

        public T Peek<T>(string key) where T : class
        {
            if (TempData.ContainsKey(key))
            {
                var result = JsonConvert.DeserializeObject<T>(TempData.Peek(key) as string);
                return result;
            }
            return default;
        }

        public void Add(string key, object value)
        {
            var val = value.ToJson();

            TempData[key] = val;
        }

        public bool Remove(string key) => TempData.ContainsKey(key) && TempData.Remove(key);
    }
}
