using System.ComponentModel.DataAnnotations;

namespace LearningManagementSystem.Models
{
    public class Department
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
