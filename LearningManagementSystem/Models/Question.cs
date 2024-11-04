namespace LearningManagementSystem.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int Favorites {  get; set; }
        public int LessionId { get; set; }
        public Lession Lession { get; set; }
    }
}
