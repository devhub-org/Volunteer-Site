using Microsoft.AspNetCore.Http;

namespace Core.DTO
{
    public class RegisterUserDTO
    {
        public string Email { get; set; }
        public string Password { get; set; } // add validation
        public string Name { get; set; }
        public string Surname { get; set; }
        public IFormFile Avatar { get; set; }
    }
}
