using Microsoft.AspNetCore.Http;
using DataProcessor.Common.Models;

namespace DataProcessor.Core.Interfaces
{
    public interface IDataService
    {
        Task<List<DataItem>> ParseFileAsync(IFormFile file);
        List<DataItem> GetUpdatedData();

    }
}
