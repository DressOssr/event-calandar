using EventApi.http.Responses;
using EventApi.Models;
using EventApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace EventApi.Services
{
    public class EventService : IEventService
    {
        private readonly CalendarEventDbContext _eventDbcontex;

        public EventService(CalendarEventDbContext eventDbcontex)
        {
            this._eventDbcontex = eventDbcontex;
        }

        public async Task<DeleteEventResponse> DeleteEvent(int eventId, int userId)
        {
            try
            {
                var calendarEvent = await _eventDbcontex.Events.FindAsync(eventId);
                if (calendarEvent == null)
                {
                    return new DeleteEventResponse
                    {
                        Success = false,
                        ErrorMessage = "Event not found",
                        ErrorCode = "DeleteEvent"
                    };
                }

                if (calendarEvent.UserId != userId)
                {
                    return new DeleteEventResponse
                    {
                        Success = false,
                        ErrorMessage = "You no have access to delete event",
                        ErrorCode = "DeleteEvent"
                    };
                }
                _eventDbcontex.Events.Remove(calendarEvent);
                await _eventDbcontex.SaveChangesAsync();
                return new DeleteEventResponse
                {
                    Success = true,
                    EventId = calendarEvent.Id
                };
            }
            catch(Exception e)
            {
                return new DeleteEventResponse
                {
                    Success = false,
                    ErrorMessage = e.Message,
                    ErrorCode = "DeleteEvent"
                };
            }
            
        }

        public async Task<GetEventsResponse> GetEvents(int userId)
        {
            try
            {
                var events = await _eventDbcontex.Events.Where(e => e.UserId == userId).ToListAsync();

                return new GetEventsResponse { Success = true, Events = events };
            }
            catch (Exception e)
            {
                return new GetEventsResponse { 
                    Success = true, 
                    ErrorMessage = e.Message,
                    ErrorCode = "GetEvents"
                };
            }
            
        }

        public async Task<SaveEventResponse> SaveEvent(Event calendarEvent)
        {
            try
            {
                if (calendarEvent.Id == 0)
                {
                    await _eventDbcontex.Events.AddAsync(calendarEvent);
                }
                else
                {
                    var eventRecord = await _eventDbcontex.Events.FindAsync(calendarEvent.Id);

                    eventRecord.Description = calendarEvent.Description;
                    eventRecord.EventStart = calendarEvent.EventStart;
                    eventRecord.EventEnd= calendarEvent.EventEnd;
                    eventRecord.EventName = calendarEvent.EventName;
                    eventRecord.Timestamp = calendarEvent.Timestamp;
                }

                await _eventDbcontex.SaveChangesAsync();

                return new SaveEventResponse
                {
                    Success = true,
                    Event = calendarEvent
                };
            }
            catch (Exception e)
            {
                return new SaveEventResponse
                {
                    Success = false,
                    ErrorMessage = e.Message,
                    ErrorCode = "SaveEvent"
                };
            }
        }
    }
}
