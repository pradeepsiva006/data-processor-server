using Microsoft.AspNetCore.Mvc;
using DataProcessor.Core.Interfaces;

namespace DataProcessor.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController : ControllerBase
    {
        private readonly IDataService _dataService;

        public DataController(IDataService dataService)
        {
            _dataService = dataService; 
        }

        [HttpPost("parse-file")]
        public async Task<IActionResult> ParseFileAsync([FromForm] IFormFile file)
        {
             return Ok(await _dataService.ParseFileAsync(file));
            
        }

        [HttpGet("updated-data")]
        public IActionResult GetUpdatedData()
        {
             return Ok(_dataService.GetUpdatedData());
        }

    }
}
