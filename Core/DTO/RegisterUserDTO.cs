using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class RegisterUserDTO
    {
        public string Email { get; set; }
        public string Password { get; set; } // add validation
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
