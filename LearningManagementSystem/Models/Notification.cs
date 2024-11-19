using System.ComponentModel.DataAnnotations;

namespace LearningManagementSystem.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public string Name { get; set; }
        public bool IsReaded { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
