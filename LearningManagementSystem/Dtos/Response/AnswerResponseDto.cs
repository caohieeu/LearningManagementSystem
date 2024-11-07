using LearningManagementSystem.Models;

namespace LearningManagementSystem.Dtos.Response
{
    public class AnswerResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public bool IsShow { get; set; }
        public DateTime DateTime { get; set; }
        public int FavoriteQuantity {  get; set; }
        public UserResponseDto User { get; set; }
    }
}
