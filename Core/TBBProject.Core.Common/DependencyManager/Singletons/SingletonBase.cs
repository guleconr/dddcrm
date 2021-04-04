namespace TBBProject.Core.Common
{
    using System;
    using System.Collections.Generic;
    public class SingletonBase
    {
        static SingletonBase()
        {
            Singletons = new Dictionary<Type, object>();
        }

        public static IDictionary<Type, object> Singletons { get; }
    }
}
