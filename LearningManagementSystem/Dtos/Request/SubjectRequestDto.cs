using LearningManagementSystem.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LearningManagementSystem.Dtos.Request
{
    public class SubjectRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateOfSubmitForApprove { get; set; }
        public int AcademicYearId { get; set; }
        public string Note { get; set; }
        public string DepartmentId { get; set; }
        public string UserId { get; set; }
    }
}
