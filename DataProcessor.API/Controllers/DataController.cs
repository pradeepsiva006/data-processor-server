using Microsoft.AspNetCore.Mvc;
using DataProcessor.Core.Interfaces;

namespace DataProcessor.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    /// <summary>
    /// Web API controller for managing data parsing and retrieval.
    /// </summary>
    public class DataController : ControllerBase
    {
        private readonly IDataService _dataService;

        public DataController(IDataService dataService)
        {
            _dataService = dataService; 
        }

        /// <summary>
        /// Uploads and parses data from a file asynchronously.
        /// </summary>
        /// <param name="file">The uploaded file containing data to be parsed.</param>
        /// <returns>
        /// A task that returns an OkObjectResult containing a list of parsed DataItems on success, 
        /// or a BadRequestObjectResult containing the parsing error message if the file is invalid or an error occurs during parsing.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if the file parameter is null.</exception>
        [HttpPost("parse-file")]
        public async Task<IActionResult> ParseFileAsync([FromForm] IFormFile file)
        {
             return Ok(await _dataService.ParseFileAsync(file));
            
        }

        /// <summary>
        /// Retrieves and returns a list of updated data from the data service.
        /// </summary>
        /// <returns>
        /// An OkObjectResult containing a list of DataItems representing the updated data, 
        /// or an InternalServerErrorResult containing the error message if an error occurs during data retrieval.
        /// </returns>
        [HttpGet("updated-data")]
        public IActionResult GetUpdatedData()
        {
             return Ok(_dataService.GetUpdatedData());
        }

    }
}
