using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningManagementSystem.Models
{
    public class Subject
    {
        [Required]
        [RegularExpression(@"^#DLK\d+$", 
            ErrorMessage = "Mã môn học phải bắt đầu bằng #KDL")]
        public string Id { get; set; }
        public string Name { get; set; }
        public string TeacherId { get; set; }
        [ForeignKey("TeacherId")]
        public ApplicationUser Teacher { get; set; }
        public string Description { get; set; }
        public DateTime DateOfSubmitForApprove { get; set; }
        public string StatusOfSubjectDoc { get; set; }
        public int DocAwaitAprrove { get; set; }
    }
}
