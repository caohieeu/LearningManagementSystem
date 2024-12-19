using Newtonsoft.Json;

namespace LearningManagementSystem.Utils
{
    public class ResponseEntity
    {
        public int code {  get; set; }
        public string message { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object data { get; set; }
    }
}
