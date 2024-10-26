using System.ComponentModel.DataAnnotations;

namespace LearningManagementSystem.Models
{
    public class AcademicYear
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
    }
}
