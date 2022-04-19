using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Author : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Location { get; set; }
        public double Rating { get; set; }
        public string Avatar { get; set; }
        public IEnumerable<Table> Tables { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
