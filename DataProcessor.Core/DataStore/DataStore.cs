using DataProcessor.Common.Models;

namespace DataProcessor.Core.DataStore
{
    public class DataStore : IDataStore
    {
        private readonly object _lock = new object();
        private List<DataItem> _data = new List<DataItem>();

        public void UpdateData(List<DataItem> newData)
        {
            lock (_lock)
            {
                _data = newData;
            }
        }

        public List<DataItem> GetData()
        {
            lock (_lock)
            {
                return new List<DataItem>(_data);
            }
        }
    }
}
