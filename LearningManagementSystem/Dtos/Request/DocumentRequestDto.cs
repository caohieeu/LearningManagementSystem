using LearningManagementSystem.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningManagementSystem.Dtos.Request
{
    public class DocumentRequestDto
    {
        public int LessionId { get; set; }
        public int DocumentId { get; set; }
        public FileUploadDto FileUpload { get; set; }
    }
}
