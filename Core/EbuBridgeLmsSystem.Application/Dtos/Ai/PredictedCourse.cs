using System.Text.Json.Serialization;

namespace EbuBridgeLmsSystem.Application.Dtos.Ai
{
    public class PredictionResponse
    {
        public string predicted_course { get; set; }
        public string Feedback { get; set; }
        public DerivedMetrics derived_metrics { get; set; }
    }

    public class DerivedMetrics
    {
        public int effort_level { get; set; }
        public int reliability_score { get; set; }
        public string course_category { get; set; }
    }
}
