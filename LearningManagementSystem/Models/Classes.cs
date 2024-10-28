using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningManagementSystem.Models
{
    public class Classes
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
