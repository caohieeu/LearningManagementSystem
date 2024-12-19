using LearningManagementSystem.Dtos.Response;
using LearningManagementSystem.Models;

namespace LearningManagementSystem.Dtos
{
    public class TitleStudentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public LessionResponseDto Lessions { get; set; }
    }
}   
