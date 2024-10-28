using LearningManagementSystem.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningManagementSystem.Dtos.Request
{
    public class DocumentRequestDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Approver { get; set; }
        public string Status { get; set; }
        public DateTime SentDate { get; set; }
        public bool IsAprroved { get; set; }
        public int FileDetailId { get; set; }
    }
}
