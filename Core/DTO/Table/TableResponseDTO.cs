using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO.Table
{
    public class TableResponseDTO
    {
        public string Name { get; set; }
        public string Describe { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public bool IsActive { get; set; }
        public string AuthorId { get; set; }
    }
}
