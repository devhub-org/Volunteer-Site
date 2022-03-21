using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class AuthorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public double Rating { get; set; }
        public string Avatar { get; set; }
        public IEnumerable<Table> Tables { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
