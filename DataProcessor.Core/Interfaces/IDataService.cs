using Microsoft.AspNetCore.Http;
using DataProcessor.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessor.Core.Interfaces
{
    public interface IDataService
    {
        Task<List<DataItem>> ParseFileAsync(IFormFile file);
        List<DataItem> GetUpdatedData();

    }
}
