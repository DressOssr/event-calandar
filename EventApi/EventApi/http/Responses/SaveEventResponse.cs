using EventApi.Models;

namespace EventApi.http.Responses
{
    public class SaveEventResponse : BaseResponse
    {
        public Event Event { get; set; }
    }
}
