using LearningManagementSystem.Models;

namespace LearningManagementSystem.Dtos.Response
{
    public class QuestionResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int Favorites { get; set; }
        public string UserId { get; set; }
    }
}
