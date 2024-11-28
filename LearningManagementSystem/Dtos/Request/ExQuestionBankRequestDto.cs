using LearningManagementSystem.Utils;

namespace LearningManagementSystem.Dtos.Request
{
    public class ExQuestionBankRequestDto
    {
        public string SubjectId { get; set; }
        public string Name { get; set; } = "Trắc nghiệm";
        public int Quantity { get; set; }
        public double PointScale { get; set; }
        public int CntQuestionEasy { get; set; }
        public int CntQuestionMedium { get; set; }
        public int CntQuestionHard { get; set; }
        public TimeSpan Duration { get; set; }
        //public LevelType Level { get; set; }
    }
}
