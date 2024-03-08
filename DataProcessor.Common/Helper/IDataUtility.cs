using DataProcessor.Common.Models;

namespace DataProcessor.Common.Helper
{
    public interface IDataUtility
    {
        DataItem ParseLine(string line);
        string GetColorCode(string colorName);
        bool IsValidColor(string colorName);
    }
}
