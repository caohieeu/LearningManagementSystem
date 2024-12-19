using LearningManagementSystem.Models;
using Newtonsoft.Json;

namespace LearningManagementSystem.Dtos
{
    public class SubjectDetailDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Teacher { get; set; }
        public List<TitleStudentDto> Titles { get; set; }
    }
}
