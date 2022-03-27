using Core.DTO;
using Core.Interfaces.CustomServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly ILogger<AuthorController> _logger;
        public AuthorController(IAuthorService authorService, ILogger<AuthorController> logger)
        {
            _authorService = authorService;
            _logger = logger;
        }
        [HttpGet]
        [ResponseCache(Duration = 30)]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<AuthorDTO>>> Get()
        {
            return Ok(await _authorService.Get());
        }
        [HttpGet("{id:int}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<AuthorDTO>> Get(int id)
        {
            var author = await _authorService.GetAuthorById(id);
            _logger.LogInformation($"Got a author with id {id}");
            return author;
        }
        [HttpGet]
        [Route("get-all")]
        public async Task<ActionResult<IEnumerable<AuthorTablesDTO>>> GetAllTables(int id)
        {
            var tables = await _authorService.GetAuthorTables(id);
            return Ok(tables);
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AuthorDTO author)
        {
            await _authorService.Create(author);
            _logger.LogInformation("Author was successfully created!");
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] AuthorDTO author)
        {
            await _authorService.Edit(author);
            _logger.LogInformation("Author was successfully updated!");
            return Ok();
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _authorService.Delete(id);
            _logger.LogInformation($"Successfully delete author with id {id}");
            return Ok();
        }
    }
}
