using DataProcessor.Common.Models;

namespace DataProcessor.Core.DataStore
{
    public interface IDataStore
    {
        void UpdateData(List<DataItem> newData);
        List<DataItem> GetData();
    }
}
