using System.ComponentModel.DataAnnotations;

namespace Flashy.Models
{
    public class FlashCard
    {
        public int Id { get; set; }
        public int SetId { get; set; }
        public int UserId { get; set; }
        [Required]
        public string Question { get; set; }
        [Required]
        public string Answer { get; set; }
        public DateTime DateCreated { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}
