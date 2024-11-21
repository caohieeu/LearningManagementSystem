using LearningManagementSystem.Models;

namespace LearningManagementSystem.Dtos.Request
{
    public class ExaminationRequestDto
    {
        public string FormExam { get; set; }
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }    
        public string SubjectId { get; set; }
        public List<QuestionAxamRequestDto> Questions { get; set; } = 
            new List<QuestionAxamRequestDto>();
    }
}
