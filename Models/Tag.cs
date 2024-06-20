using System.ComponentModel.DataAnnotations;

namespace Flashy.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [Required]
        public string Label { get; set; }
        public ICollection<Flashcard> Flashcards { get; set; }
    }
}
