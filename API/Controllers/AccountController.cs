using Core.DTO;
using Core.Entities;
using Core.Interfaces.CustomServices;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<Author> _userManager;
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
