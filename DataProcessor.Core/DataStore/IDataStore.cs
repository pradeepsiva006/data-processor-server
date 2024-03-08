using DataProcessor.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessor.Core.DataStore
{
    public interface IDataStore
    {
        void UpdateData(List<DataItem> newData);
        List<DataItem> GetData();
    }
}
