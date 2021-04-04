namespace TBBProject.Core.Common
{
    using System;
    using System.Collections.Generic;

    public static class CollectionExtensions
    {
        /// <summary>
        ///     Adds an Item to the collection if the predicate is met.
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="collection">collection</param>
        /// <param name="item">item</param>
        /// <param name="predicate">predicate</param>
        public static void AddIf<T>(this ICollection<T> collection, T item, Func<T, bool> predicate)
        {
            if (predicate(item))
            {
                collection.Add(item);
            }
        }

        /// <summary>
        ///     Adds enumerable of items (range) to the collection.
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="collection">collection</param>
        /// <param name="items">items</param>
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items) => items.ForEach(collection.Add);

        /// <summary>
        ///     Adds enumerable of items (range) to the collection.
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="collection">collection</param>
        /// <param name="items">items</param>
        public static void AddRange<T>(this ICollection<T> collection, params T[] items) => items.ForEach(collection.Add);

        /// <summary>
        ///     Remove items from collection.
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="collection">collection</param>
        /// <param name="items">items</param>
        public static void RemoveRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            if (items.IsNullOrEmpty())
            {
                return;
            }

            foreach (var item in items)
            {
                if (collection.Contains(item))
                {
                    collection.Remove(item);
                }
            }
        }

    }
}
