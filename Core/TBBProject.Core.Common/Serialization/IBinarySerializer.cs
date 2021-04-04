namespace TBBProject.Core.Common
{
    public interface IBinarySerializer
    {
        byte[] Serialize<T>(T value)
            where T : class;

        T Deserialize<T>(byte[] bytes)
            where T : class;
    }
}
