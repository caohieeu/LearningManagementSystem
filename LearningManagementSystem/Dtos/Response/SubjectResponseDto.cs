namespace LearningManagementSystem.Dtos.Response
{
    public class SubjectResponseDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateOfSubmitForApprove { get; set; }
        public string StatusOfSubjectDoc { get; set; }
        public int DocAwaitAprrove { get; set; }
        public string Department { get; set; }
    }
}
