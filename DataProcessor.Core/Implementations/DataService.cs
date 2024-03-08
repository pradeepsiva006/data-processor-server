using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using DataProcessor.Common.Helper;
using DataProcessor.Common.Models;
using DataProcessor.Core.DataStore;
using DataProcessor.Core.Interfaces;

namespace DataProcessor.Core.Implementations
{
    public class DataService : IDataService
    {
        private readonly IDataStore _dataStore;
        private readonly IDataUtility _dataUtility;
        private readonly ILogger<DataService> _logger;

        public DataService(IDataStore dataStore, IDataUtility dataUtility, ILogger<DataService> logger) { 
            _dataStore = dataStore;
            _dataUtility = dataUtility;
            _logger = logger;
        }

        #region public methods
        public async Task<List<DataItem>> ParseFileAsync(IFormFile file)
        {
            var data = new List<DataItem>();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    try
                    {
                        DataItem item = _dataUtility.ParseLine(line);
                        data.Add(item);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Error parsing in ProcessFile(): {ex.Message}");
                        throw;
                    }
                }
            }

            if(data.Count == 0)
            {
                throw new FormatException("File should contain text to parse!");
            }

            _dataStore.UpdateData(data);

            return data;
        }

        public List<DataItem> GetUpdatedData()
        {
            try
            {
                RandomizeData();
                return _dataStore.GetData();
            }

            catch(Exception ex)
            {
                _logger.LogError($"Error in GetUpdatedData() : {ex.Message}");
                throw;
            }

        }

        #endregion

        #region private methods
        private void RandomizeData()
        {
            var data = _dataStore.GetData();

            if (data.Count == 0)
            {
                return;
            }

            int maxValue = data[0].Value;
            int minValue = data[0].Value;

            foreach (var item in data)
            {
                maxValue = Math.Max(maxValue, item.Value);
                minValue = Math.Min(minValue, item.Value);
            }

            Random random = new Random();
            foreach (var item in data)
            {
                item.Value = random.Next(minValue, maxValue + maxValue);
            }

            _dataStore.UpdateData(data);
        }
        #endregion
    }
}
