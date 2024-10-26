using Microsoft.VisualBasic.FileIO;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using LearningManagementSystem.Utils;

namespace LearningManagementSystem.Models
{
    public class FileDetails
    {
        [Key]
        public int Id { get; set; }
        public string FileName { get; set; }
        public byte[] FileData { get; set; }
        public FileType FileType { get; set; }
    }
}
