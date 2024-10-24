using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningManagementSystem.Models
{
    public class FileDoc
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Size { get; set; }
        public int DocumentId { get; set; }
        [ForeignKey("DocumentId")]
        public Document Document {  get; set; }
    }
}
