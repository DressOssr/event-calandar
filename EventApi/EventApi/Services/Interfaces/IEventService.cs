using EventApi.http.Responses;
using EventApi.http.Requests;
using EventApi.Models;

namespace EventApi.Services.Interfaces
{
    public interface IEventService
    {
        Task<GetEventsResponse> GetEvents(int userId);

        Task<SaveEventResponse> SaveEvent(Event task);

        Task<DeleteEventResponse> DeleteEvent(int eventId, int userId);
    }
}
