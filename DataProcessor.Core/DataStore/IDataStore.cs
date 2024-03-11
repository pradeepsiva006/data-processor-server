using DataProcessor.Common.Models;

namespace DataProcessor.Core.DataStore
{
    /// <summary>
    /// Interface defining methods for in-memory data access.
    /// </summary>
    public interface IDataStore
    {
        /// <summary>
        /// Updates the in-memory data store with a new list of DataItems.
        /// </summary>
        /// <param name="newData">The new list of DataItems to be stored.</param>
        /// <remarks>
        /// This method uses thread-safe synchronization to update the data store.
        /// </remarks>
        void UpdateData(List<DataItem> newData);

        /// <summary>
        /// Retrieves a copy of the current data stored in memory.
        /// </summary>
        /// <returns>A copy of the list of DataItems currently stored in memory.</returns>
        /// <remarks>
        /// This method uses thread-safe synchronization to access the data store and returns a copy to prevent modification of the internal data.
        /// </remarks>
        List<DataItem> GetData();
    }
}
