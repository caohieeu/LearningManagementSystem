using Newtonsoft.Json;

namespace LearningManagementSystem.Dtos.Response
{
    public class IntrospectResponseDto
    {
        public bool Valid { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public UserResponseDto User { get; set; }
    }
}
