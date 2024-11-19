namespace LearningManagementSystem.Dtos.Response
{
    public class NotificationResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsReaded { get; set; }
        public UserResponseDto User { get; set; }
    }
}
