namespace LearningManagementSystem.Models
{
    public class Favorite
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int ItemId { get; set; }
        public string ItemType { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
