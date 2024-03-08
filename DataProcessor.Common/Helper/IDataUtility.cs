using DataProcessor.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessor.Common.Helper
{
    public interface IDataUtility
    {
        DataItem ParseLine(string line);
        string GetColorCode(string colorName);
        bool IsValidColor(string colorName);
    }
}
