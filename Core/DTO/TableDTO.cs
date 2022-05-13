﻿using Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class TableDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Describe { get; set; }
        public IFormFile Image { get; set; }
        public double Price { get; set; }
        public string AuthorId { get; set; }
    }
}
