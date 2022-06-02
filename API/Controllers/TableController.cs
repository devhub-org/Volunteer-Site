using Core.DTO;
using Core.Helpers;
using Core.Interfaces.CustomServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Core.DTO.Table;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly ITableService _tableService;
        private readonly ILogger<TableController> _logger;
        private readonly IMapper _mapper;

        public TableController(ITableService tableService,
            ILogger<TableController> logger, IMapper mapper)
        {
            _tableService = tableService;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        [ResponseCache(Duration = 30)]

        public async Task<ActionResult<IEnumerable<TableDTO>>> Get()
        {
            //string userId = HttpContext.User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value;

            //_logger.LogInformation();
            return Ok(await _tableService.Get());
        }
        [HttpGet("{id:int}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<TableResponseDTO>> Get(int id)
        {
            var track = await _tableService.GetTableById(id);
            _logger.LogInformation($"Got a table with id {id}");
            return track;
        }

        [HttpPost("Create")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Create([FromForm] TableDTO table)
        {
            if (!ModelState.IsValid) return BadRequest("Model is invalid");

            await _tableService.Create(table);

            _logger.LogInformation("Table was successfully created!");


            return Ok(); // also can return all table

        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Put([FromBody] TableDTO table)
        {
            await _tableService.Edit(table);
            _logger.LogInformation("Table was successfully updated!");
            return Ok();
        }
        [HttpDelete("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Delete(int id)
        {
            await _tableService.Delete(id);
            _logger.LogInformation($"Successfully delete table with id {id}");
            return Ok();
        }
    }
}
