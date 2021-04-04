using System.Diagnostics.CodeAnalysis;

namespace TBBProject.Core.Common
{
    [ExcludeFromCodeCoverage]
    public class Singleton<T> : SingletonBase
    {
        private static T instance;

        public static T Instance
        {
            get => instance;
            set
            {
                instance = value;
                Singletons[typeof(T)] = value;
            }
        }
    }
}
