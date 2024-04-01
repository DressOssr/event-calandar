using System.Text.Json.Serialization;

namespace EventApi.http.Responses
{
    public class DeleteEventResponse : BaseResponse
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int EventId { get; set; }
    }
}
