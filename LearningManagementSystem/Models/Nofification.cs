using System.ComponentModel.DataAnnotations;

namespace LearningManagementSystem.Models
{
    public class Nofification
    {
        [Key]
        public int Id { get; set; }
        public int Title { get; set; }
        public int Detail { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
