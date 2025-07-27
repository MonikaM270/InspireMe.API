using System.ComponentModel.DataAnnotations;

namespace InspireMe.API.Models
{
    public class Quote
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Mood is required")]
        [MinLength(5, ErrorMessage = "Mood must be at least 3 character's long")]
        public required string Mood { get; set; }

        [Required(ErrorMessage = "Quote text is required")]
        [MinLength(5, ErrorMessage = "Quote must be at least 5 character's long")]
        public required string Text { get; set; }
    }
}
