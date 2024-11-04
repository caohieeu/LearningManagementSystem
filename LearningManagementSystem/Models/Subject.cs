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
        public string Description { get; set; } = string.Empty;
        public DateTime DateOfSubmitForApprove { get; set; }
        public string StatusOfSubjectDoc { get; set; } = string.Empty;
        //public int DocAwaitAprrove { get; set; }
        public string Note { get; set; } = string.Empty;
        public int AcademicYearId { get; set; }
        [ForeignKey("AcademicYearId")]
        public AcademicYear AcademicYear { get; set; } = null!;
        public string DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
