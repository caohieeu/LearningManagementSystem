using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningManagementSystem.Models
{
    public class Title
    {
        [Key]
        public int Id {  get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public string SubjectId { get; set; }
        [ForeignKey(nameof(SubjectId))]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Subject Subject { get; set; }
    }
}
