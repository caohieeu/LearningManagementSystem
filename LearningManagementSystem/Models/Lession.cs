using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningManagementSystem.Models
{
    public class Lession
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public int TitleId {  get; set; }
        [ForeignKey("TitleId")]
        public Title Title { get; set; }
        public string ClassId { get; set; }
        [ForeignKey("ClassId")]
        public Classes Classes { get; set; }
    }
}
