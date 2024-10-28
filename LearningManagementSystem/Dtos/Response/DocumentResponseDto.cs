using LearningManagementSystem.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningManagementSystem.Dtos.Response
{
    public class DocumentResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Approver { get; set; }
        public string Status { get; set; }
        public DateTime SentDate { get; set; }
        public bool IsAprroved { get; set; }
        public double FileSize { get; set; }
    }
}
