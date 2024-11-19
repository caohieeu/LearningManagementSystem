using LearningManagementSystem.Utils;

namespace LearningManagementSystem.Dtos
{
    public class FilterQuestionDto
    {
        public int? LessionId { get; set; }
        public QType? TypeQuestion { get; set; }
        public FQType? FillQuestion { get; set; }
    }
}
