using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningManagementSystem.Models
{
    public class UserSubjects
    {
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }
        public string SubjectId { get; set; }   
        [ForeignKey("SubjectId")]
        public Subject Subject { get; set; }
    }
}
