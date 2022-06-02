using Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Core.DTO
{
    public class TableDTO
    {
        public string Name { get; set; }
        public string Describe { get; set; }
        public IFormFile Image { get; set; }
        public double Price { get; set; }
        public bool IsActive { get; set; } = true;
        public string AuthorId { get; set; }
    }
}
