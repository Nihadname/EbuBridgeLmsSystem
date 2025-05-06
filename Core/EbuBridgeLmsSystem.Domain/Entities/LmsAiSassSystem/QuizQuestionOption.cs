namespace EbuBridgeLmsSystem.Domain.Entities.LmsAiSassSystem
{
    public sealed class QuizQuestionOption
    {
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        public Guid QuizQuestionId { get; set; }
        public QuizQuestion QuizQuestion { get; set; }
    }
}
