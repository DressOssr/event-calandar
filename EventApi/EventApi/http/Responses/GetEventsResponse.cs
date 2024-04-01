using EventApi.Models;

namespace EventApi.http.Responses
{
    public class GetEventsResponse : BaseResponse
    {
        public List<Event> Events { get; set; }
    }
}
