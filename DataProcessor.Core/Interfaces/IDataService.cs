using Microsoft.AspNetCore.Http;
using DataProcessor.Common.Models;

namespace DataProcessor.Core.Interfaces
{
    /// <summary>
    /// Interface defining methods for data parsing and manipulation.
    /// </summary>
    public interface IDataService
    {
        /// <summary>
        /// Asynchronously parses data from an uploaded file and returns the parsed data.
        /// </summary>
        /// <param name="file">The uploaded file containing data to be parsed.</param>
        /// <returns>A task that returns a list of parsed DataItems.</returns>
        /// <exception cref="ParsingException">Thrown if an error occurs during parsing or the file is empty.</exception>

        Task<List<DataItem>> ParseFileAsync(IFormFile file);

        /// <summary>
        /// Retrieves and randomizes data from the underlying data store.
        /// </summary>
        /// <returns>A list of DataItems containing the updated data.</returns>
        List<DataItem> GetUpdatedData();
    }
}
