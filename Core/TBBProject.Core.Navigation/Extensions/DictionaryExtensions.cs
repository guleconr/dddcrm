using System;
using System.Collections.Generic;

namespace TBBProject.Core.Navigation
{
    public static class DictionaryExtensions
    {
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue = default(TValue))
        {
            if (dictionary == null) { throw new Exception(nameof(dictionary)); }
            if (key == null) { throw new Exception(nameof(key)); }

            return dictionary.TryGetValue(key, out var value) ? value : defaultValue;
        }
    }
}
