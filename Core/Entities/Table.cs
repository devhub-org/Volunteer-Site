using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Table
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Describe { get; set; }
        public string Goal { get; set; }
        public string Image { get; set; }
        public int? AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
