using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Author
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
