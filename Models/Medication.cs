namespace StrokePrediction.Models
{
    public class Medication
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public TimeSpan Time { get; set; }
        public bool IsDaily { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
