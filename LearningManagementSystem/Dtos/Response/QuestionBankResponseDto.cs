using LearningManagementSystem.Utils;

namespace LearningManagementSystem.Dtos.Response
{
    public class QuestionBankResponseDto
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public QEType Type { get; set; }
        public LevelType Level { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastUpdate { get; set; }
        public string UpdatedBy { get; set; }

    }
}
