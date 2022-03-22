using Core.DTO;
using Core.Interfaces.CustomServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly ITableService _tableService;
        private readonly ILogger<TableController> _logger;
        public TableController(ITableService tableService, ILogger<TableController> logger)
        {
            _tableService = tableService;
            _logger = logger;
        }
        [HttpGet]
        [ResponseCache(Duration = 30)]
        public async Task<ActionResult<IEnumerable<TableDTO>>> Get()
        {
            return Ok(await _tableService.Get());
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TableDTO>> Get(int id)
        {
            var track = await _tableService.GetTableById(id);
            _logger.LogInformation($"Got a table with id {id}");
            return track;
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TableDTO table)
        {
            await _tableService.Create(table);
            _logger.LogInformation("Table was successfully created!");
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] TableDTO table)
        {
            await _tableService.Edit(table);
            _logger.LogInformation("Table was successfully updated!");
            return Ok();
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _tableService.Delete(id);
            _logger.LogInformation($"Successfully delete table with id {id}");
            return Ok();
        }
    }
}
