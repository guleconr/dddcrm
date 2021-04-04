namespace TBBProject.Core.UiMessages
{
    public interface ITempDataProvider
    {
        /// <summary>
        /// Get the value of a given key from TempData/>
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        T Get<T>(string key) where T : class;
        /// <summary>
        /// Peeks the key from TempData
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T Peek<T>(string key) where T : class;
        /// <summary>
        /// Adds key and value to Temp data
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void Add(string key, object value);
        /// <summary>
        /// Remove value with a given key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Remove(string key);
    }
}
