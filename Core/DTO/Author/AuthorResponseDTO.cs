using System;
using System.Collections.Generic;
using Core.Entities;

namespace Core.DTO.Author
{
    public class AuthorResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public double Rating { get; set; }
        public string Avatar { get; set; }
        public IEnumerable<Entities.Table> Tables { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
