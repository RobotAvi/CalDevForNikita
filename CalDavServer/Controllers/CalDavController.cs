using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using CalDavServer.Services;
using CalDavServer.Utils;
using CalDavServer.Models;

namespace CalDavServer.Controllers
{
    [ApiController]
    [Route("caldav")]
    public class CalDavController : ControllerBase
    {
        private readonly CalendarService _calendarService;
        private readonly EventService _eventService;
        public CalDavController(CalendarService calendarService, EventService eventService)
        {
            _calendarService = calendarService;
            _eventService = eventService;
        }

        // TODO: Реализовать обработку PROPFIND, REPORT, PUT, DELETE, MKCALENDAR и др.
        [HttpGet("ping")]
        public IActionResult Ping() => Ok("CalDAV server is running");

        [HttpPropFind("")]
        public IActionResult PropFind()
        {
            var calendars = _calendarService.GetAll().Select(c => c.Name).ToList();
            var xml = WebDavHelper.BuildPropFindResponse(calendars);
            return Content(xml, "application/xml");
        }

        [HttpPut("calendar/{id}/event/{eventId}")]
        public IActionResult PutEvent(Guid id, Guid eventId, [FromBody] Event ev)
        {
            ev.Id = eventId;
            ev.CalendarId = id;
            _eventService.Update(eventId, ev);
            return Ok();
        }

        [HttpDelete("calendar/{id}/event/{eventId}")]
        public IActionResult DeleteEvent(Guid id, Guid eventId)
        {
            _eventService.Delete(eventId);
            return Ok();
        }

        [HttpPost("calendar")]
        public IActionResult MkCalendar([FromBody] Calendar calendar)
        {
            _calendarService.Create(calendar);
            return Ok(calendar.Id);
        }

        [HttpGet("calendar/{id}/export")]
        public IActionResult ExportCalendar(Guid id)
        {
            var calendar = _calendarService.Get(id);
            if (calendar == null) return NotFound();
            var ics = IcsHelper.GenerateIcs(calendar.Events);
            return File(Encoding.UTF8.GetBytes(ics), "text/calendar", $"calendar_{id}.ics");
        }

        [HttpPost("calendar/{id}/import")]
        public IActionResult ImportCalendar(Guid id)
        {
            using (var reader = new StreamReader(Request.Body))
            {
                var ics = reader.ReadToEnd();
                var events = IcsHelper.ParseIcs(ics);
                foreach (var ev in events)
                {
                    ev.CalendarId = id;
                    _eventService.Create(ev);
                }
            }
            return Ok();
        }
    }
}