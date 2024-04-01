using EventApi.http.Requests;
using EventApi.http.Responses;
using EventApi.Models;
using EventApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TasksApi.Controllers;

namespace EventApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : BaseApiController
    {
        
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            this._eventService = eventService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var getEventResponse = await _eventService.GetEvents(UserID);

            if (!getEventResponse.Success)
            {
                return UnprocessableEntity(getEventResponse);
            }

            var eventResponse = getEventResponse.Events.ConvertAll(e => new EventResponse
            {
                Id = e.Id,
                Description = e.Description,
                EventName = e.EventName,
                EventEnd = e.EventEnd,
                EventStart = e.EventStart,
            });

            return Ok(eventResponse);
        }

        [HttpPost]
        public async Task<IActionResult> Post(EventRequest eventRequest)
        {
            var @event = new Event 
            {
                Id = eventRequest.Id,
                Description = eventRequest.Description,
                EventStart = eventRequest.EventStart, 
                EventEnd= eventRequest.EventEnd,
                EventName = eventRequest.EventName,
                Timestamp = eventRequest.Ts,
                UserId = UserID
            };

            var saveEventResponse = await _eventService.SaveEvent(@event);

            if (!saveEventResponse.Success)
            {
                return UnprocessableEntity(saveEventResponse);
            }

            var eventResponse = new EventResponse 
            { 
                Id = saveEventResponse.Event.Id, 
                Description = saveEventResponse.Event.Description,
                EventEnd = saveEventResponse.Event.EventEnd, 
                EventStart = saveEventResponse.Event.EventStart, 
                EventName = saveEventResponse.Event.EventName, 
            };

            return Ok(eventResponse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest(new DeleteEventResponse 
                { 
                    Success = false,
                    ErrorCode = "EventDelete", 
                    ErrorMessage = "Invalid Task id" 
                });
            }
            var deleteTaskResponse = await _eventService.DeleteEvent(id, UserID);
            if (!deleteTaskResponse.Success)
            {
                return UnprocessableEntity(deleteTaskResponse);
            }

            return Ok(deleteTaskResponse.EventId);
        }

        [HttpPut]
        public async Task<IActionResult> Put(EventRequest eventRequest)
        {
            var @event = new Event
            {
                Id = eventRequest.Id,
                Description = eventRequest.Description,
                EventStart = eventRequest.EventStart,
                EventEnd = eventRequest.EventEnd,
                EventName = eventRequest.EventName,
                Timestamp = eventRequest.Ts,
                UserId = UserID
            };
            var saveTaskResponse = await _eventService.SaveEvent(@event);

            if (!saveTaskResponse.Success)
            {
                return UnprocessableEntity(saveTaskResponse);
            }

            var eventResponse = new EventResponse
            {
                Id = saveTaskResponse.Event.Id,
                Description = saveTaskResponse.Event.Description,
                EventEnd = saveTaskResponse.Event.EventEnd,
                EventStart = saveTaskResponse.Event.EventStart,
                EventName = saveTaskResponse.Event.EventName,
            };
            return Ok(eventResponse);
        }
    }
}
