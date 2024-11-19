namespace LearningManagementSystem.Models
{
    public class Examination
    {
        public string Id {  get; set; }
        public string FormExam {  get; set; }
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
        public string SubjectId { get; set; }
        public Subject Subject { get; set; }
        public bool IsApprove { get; set; }
        public string Status { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreateBy { get; set; }
    }
}
