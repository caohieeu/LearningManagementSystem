using LearningManagementSystem.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LearningManagementSystem.Dtos.Request
{
    public class SubjectRequestDto
    {
        public string Name { get; set; }
        public string TeacherId { get; set; }
        public string Description { get; set; }
        public DateTime DateOfSubmitForApprove { get; set; }
        public string StatusOfSubjectDoc { get; set; }
        public int DocAwaitAprrove { get; set; }
    }
}
