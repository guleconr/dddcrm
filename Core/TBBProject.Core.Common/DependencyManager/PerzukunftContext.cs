namespace TBBProject.Core.Common
{
    using System.Runtime.CompilerServices;

    public class TBBProjectContext
    {
        public static IManageDependencies Current
        {
            get
            {
                if (Singleton<IManageDependencies>.Instance == null)
                {
                    Create();
                }

                return Singleton<IManageDependencies>.Instance;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IManageDependencies Create()
        {
            return Singleton<IManageDependencies>.Instance ?? (Singleton<IManageDependencies>.Instance = new DependencyManager());
        }

        public static void Replace(IManageDependencies dependencyManager)
        {
            Singleton<IManageDependencies>.Instance = dependencyManager;
        }
    }
}
