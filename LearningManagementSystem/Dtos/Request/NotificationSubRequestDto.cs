namespace LearningManagementSystem.Dtos.Request
{
    public class NotificationSubRequestDto
    {
        public string Title { get; set; }
        public string Detail { get; set; }
        public string? Name { get; set; }
        public string SubjectId { get; set; }
        public List<string> UsersId { get; set; }
    }
}
