using System.ComponentModel.DataAnnotations.Schema;

namespace StrokePrediction.Models
{
    public class Medication
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string Name { get; set; }
        public TimeSpan Time { get; set; }
        public bool IsDaily { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
