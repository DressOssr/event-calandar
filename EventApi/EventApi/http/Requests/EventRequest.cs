namespace EventApi.http.Requests;

public class EventRequest
{
    public int Id { get; set; }
    public string EventName { get; set; }
    public string Description { get; set; }
    public DateTime EventStart { get; set; }
    public DateTime EventEnd { get; set; }
    public DateTime Ts { get; set; }
}
