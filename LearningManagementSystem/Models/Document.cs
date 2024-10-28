using LearningManagementSystem.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningManagementSystem.Models
{
    public class Document
    {
        [Key]
        public int Id { get; set; }
        public string FileName { get; set; }
        public byte[] FileData { get; set; }
        public FileType FileType { get; set; }
        public double Size { get; set; }
        public DateTime LastUpdate { get; set; }
        public string Type { get; set; }
        public string Approver { get; set; }
        public string Status { get; set; }
        public DateTime SentDate { get; set; }
        public bool IsAprroved { get; set; }
    }
}
