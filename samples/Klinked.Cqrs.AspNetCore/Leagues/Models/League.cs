using System.ComponentModel.DataAnnotations;

namespace Klinked.Cqrs.AspNetCore.Leagues.Models
{
    public class League
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }
    }
}