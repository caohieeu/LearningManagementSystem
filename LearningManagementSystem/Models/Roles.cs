using System.ComponentModel.DataAnnotations;

namespace LearningManagementSystem.Models
{
    public class Roles
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
