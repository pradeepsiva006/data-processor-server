using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using DataProcessor.Common.Helper;
using DataProcessor.Common.Models;
using DataProcessor.Core.DataStore;
using DataProcessor.Core.Interfaces;
using DataProcessor.Common.CustomExceptions;

namespace DataProcessor.Core.Implementations
{
    /// <summary>
    /// Service class responsible for data parsing and manipulation.
    /// </summary>
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

        /// <summary>
        /// Asynchronously parses data from an uploaded file.
        /// </summary>
        /// <param name="file">The uploaded file containing data to be parsed.</param>
        /// <returns>A task that returns a list of parsed DataItems.</returns>
        /// <exception cref="ParsingException">Thrown if an error occurs during parsing or the file is empty.</exception>

        #region public methods
        public async Task<List<DataItem>> ParseFileAsync(IFormFile file)
        {
            if (file.Length == 0)
            {
                throw new ParsingException("File should contain text to parse!");
            }
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
                    catch (ParsingException ex)
                    {
                        _logger.LogError($"Error parsing the line - {line} in ParseFileAsync(): {ex?.InnerException?.Message}");
                        throw;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Unexpected Error parsing the line - {line} in ParseFileAsync(): {ex.Message}");
                        throw;
                    }
                }
            }

            _dataStore.UpdateData(data);

            return data;
        }

        /// <summary>
        /// Retrieves and randomizes data from the underlying data store.
        /// </summary>
        /// <returns>A list of DataItems containing the updated data.</returns>

        public List<DataItem> GetUpdatedData()
        {
            try
            {
                RandomizeData();
                return _dataStore.GetData();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected Error in GetUpdatedData() : {ex.Message}");
                throw;
            }

        }
        #endregion

        #region private methods
        /// <summary>
        /// Randomizes the values of the data retrieved from the data store.
        /// </summary>
        /// <remarks>
        /// This method modifies the data retrieved from the data store in-place.
        /// </remarks>
        /// <exception cref="ArgumentNullException">Thrown if the data retrieved from the data store is null.</exception>

        private void RandomizeData()
        {
            try
            {
                var data = _dataStore.GetData();

                if (data.Count == 0)
                {
                    return;
                }

                int maxValue = data.Max(item => item.Value);
                int minValue = data.Min(item => item.Value);

                Random random = new Random();
                foreach (var item in data)
                {
                    item.Value = random.Next(minValue, maxValue + maxValue);
                }

                _dataStore.UpdateData(data);

            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError($"ArgumentNullException in RandomizeData() => {ex.Message}");
                throw;
            }
            catch(Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
