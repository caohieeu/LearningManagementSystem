using LearningManagementSystem.Utils;

namespace LearningManagementSystem.Dtos
{
    public class FilterQExaminationDto
    {
        public string DepartmentId {  get; set; }
        public string? SubjectId { get; set; }
        public List<LevelType>? Levels { get; set; }
    }
}
