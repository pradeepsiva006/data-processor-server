using DataProcessor.Common.Models;

namespace DataProcessor.Core.DataStore
{
    /// <summary>
    /// Singleton class providing in-memory data storage and retrieval functionalities.
    /// </summary>
    public class DataStore : IDataStore
    {
        private readonly object _lock = new object();
        private List<DataItem> _data = new List<DataItem>();

        /// <summary>
        /// Updates the in-memory data store with a new list of DataItems.
        /// </summary>
        /// <param name="newData">The new list of DataItems to be stored.</param>
        /// <remarks>
        /// Uses thread-safe synchronization to update the data store.
        /// </remarks>

        public void UpdateData(List<DataItem> newData)
        {
            lock (_lock)
            {
                _data = newData;
            }
        }

        /// <summary>
        /// Retrieves a copy of the current data stored in memory.
        /// </summary>
        /// <returns>A copy of the list of DataItems currently stored in memory.</returns>
        /// <remarks>
        /// Uses thread-safe synchronization to access the data store and returns a copy to prevent modification of the internal data.
        /// </remarks>
        public List<DataItem> GetData()
        {
            lock (_lock)
            {
                return new List<DataItem>(_data);
            }
        }
    }
}
