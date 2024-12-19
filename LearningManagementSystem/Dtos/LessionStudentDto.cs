using LearningManagementSystem.Models;
using Newtonsoft.Json;

namespace LearningManagementSystem.Dtos
{
    public class LessionStudentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Document Video { get; set; }
    }
}
