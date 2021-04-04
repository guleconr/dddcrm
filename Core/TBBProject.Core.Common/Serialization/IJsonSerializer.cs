namespace TBBProject.Core.Common
{
    using Newtonsoft.Json;

    public interface IJsonSerializer
    {
        string Serialize(object obj);

        T Deserialize<T>(string json);

        string Serialize(JsonTextWriter writer, object obj);
    }
}
