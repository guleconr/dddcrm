namespace TBBProject.Core.Common
{
    using System.IO;
    using Newtonsoft.Json;

    public class JsonSerializer : IJsonSerializer
    {
        private static readonly string JsonContentType = "application/json";

        private readonly Newtonsoft.Json.JsonSerializer _serializer;

        private readonly JsonSerializerSettings _settings;

        public string ContentType { get; set; }

        public JsonSerializer(JsonSerializerSettings settings)
        {
            ContentType = JsonContentType;

            _settings = settings;

            this._serializer = new Newtonsoft.Json.JsonSerializer
            {
                MissingMemberHandling = _settings.MissingMemberHandling,
                Formatting = _settings.Formatting,
                NullValueHandling = _settings.NullValueHandling,
                DefaultValueHandling = _settings.DefaultValueHandling,
                DateFormatString = "yyyy-MM-dd HH:mm:ss.FFFFFFF" 
            };
        }

        public T Deserialize<T>(string json)
        {
            Ensure.That("json", json).Is.NotNull().NotEmpty();

            using (var stringReader = new StringReader(json))
            {
                using (var jsonTextReader = new JsonTextReader(stringReader))
                {
                    return this._serializer.Deserialize<T>(jsonTextReader);
                }
            }
        }

        public string Serialize(object obj)
        {
            Ensure.That("object", obj).Is.NotNull();

            using (var stringWriter = new StringWriter())
            {
                using (var jsonTextWriter = new JsonTextWriter(stringWriter))
                {
                    this._serializer.Serialize(jsonTextWriter, obj);

                    var result = stringWriter.ToString();
                    return result;
                }
            }
        }

        public string Serialize(JsonTextWriter writer, object obj)
        {
            Ensure.That("object", obj).Is.NotNull();
            Ensure.That("writer", obj).Is.NotNull();

            using (var stringWriter = new StringWriter())
            {
                this._serializer.Serialize(writer, obj);

                var result = stringWriter.ToString();
                return result;
            }
        }
    }
}
