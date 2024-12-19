using Newtonsoft.Json;

namespace LearningManagementSystem.Dtos.Response
{
    public class AuthResponseDto
    {
        public bool result {  get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string token { get; set; }
    }
}
