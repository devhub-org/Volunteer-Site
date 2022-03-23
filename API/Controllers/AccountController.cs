using Core.DTO;
using Core.Interfaces.CustomServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterUserDTO data)
        {
            await accountService.RegisterAsync(data);
            return Ok("Successfully created new user!");
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthorizationDTO>> Login([FromBody] UserLoginDTO data)
        {
            return await accountService.LoginAsync(data.Email, data.Password);
        }
    }
}
