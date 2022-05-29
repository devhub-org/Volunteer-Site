using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Table
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Describe { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string AuthorId { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(500_000, ErrorMessage = "You can't ask more than 500,000 people!")]
        public double Price { get; set; }
        [Required]
        public bool IsActive { get; set; } = true;
    }
}
