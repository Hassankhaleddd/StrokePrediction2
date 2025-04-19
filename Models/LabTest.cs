using System.ComponentModel.DataAnnotations.Schema;

namespace StrokePrediction.Models
{
    public class LabTest
    {
        public int Id { get; set; }
        [ForeignKey("UserId")]
        public string UserId { get; set; } 
        public ApplicationUser User { get; set; }

        public string Gender { get; set; }
        public int Age { get; set; }
        public int Hypertension { get; set; }
        public int HasHeartDisease { get; set; }
        public bool EverMarried { get; set; }
        public string WorkType { get; set; }
        public string Residence_type { get; set; }
        public float avg_glucose_level { get; set; }
        public float Bmi { get; set; }
        public string smoking_status { get; set; }

        public int StrokeResult { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
