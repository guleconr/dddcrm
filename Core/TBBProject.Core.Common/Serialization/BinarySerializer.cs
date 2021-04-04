namespace TBBProject.Core.Common
{
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;

    public class BinarySerializer : IBinarySerializer
    {
        public byte[] Serialize<T>(T value)
            where T : class
        {
            Ensure.That("value", value).Is.NotNull();

            using (var ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, value);
                return ms.ToArray();
            }
        }

        public T Deserialize<T>(byte[] bytes)
            where T : class
        {
            Ensure.That("bytes", bytes).Is.NotNull().NotEmpty();

            using (var ms = new MemoryStream(bytes))
            {
                return (T)new BinaryFormatter().Deserialize(ms);
            }
        }
    }
}
