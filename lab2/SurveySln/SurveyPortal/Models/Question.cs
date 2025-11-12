namespace SurveyPortal.Models
{
    public class Question
    {
        public long QuestionID { get; set; }
        public string Text { get; set; } = string.Empty;
        public string QuestionType { get; set; } = "Text";

        // --- Зв'язок Один-до-Багатьох ---

        // 1. Foreign Key (Зовнішній ключ)
        public long SurveyID { get; set; }

        // 2. Navigation Property (Навігаційна властивість до "одного")
        public Survey? Survey { get; set; }
    }
}