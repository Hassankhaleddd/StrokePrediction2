namespace StrokePrediction.Models
{
    public class StrokeInput
    {
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
    }
}
