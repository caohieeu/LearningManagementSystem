using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningManagementSystem.Models
{
    public class DocumentLession
    {
        public int DocumentId { get; set; }
        public Document Document { get; set; }
        public int LessionId { get; set; }
        public Lession Lession { get; set; }
    }
}
