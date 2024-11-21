using LearningManagementSystem.Models;

namespace LearningManagementSystem.Dtos.Response
{
    public class ExaminationResponseDto
    {
        public int Id { get; set; }
        public string FormExam { get; set; }
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
        public string SubjectId { get; set; }
        public bool IsApprove { get; set; }
        public string Status { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreateBy { get; set; }
    }
}
