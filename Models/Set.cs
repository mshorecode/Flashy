using System.ComponentModel.DataAnnotations;

namespace Flashy.Models
{
    public class Set
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        public bool Favorite { get; set; }
        public DateTime DateCreated { get; set; }
        public ICollection<Flashcard> Flashcards { get; set; } = new List<Flashcard>();
    }
}
