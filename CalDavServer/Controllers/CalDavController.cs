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

        // REPORT (calendar-query, calendar-multiget, free-busy)
        [AcceptVerbs("REPORT")]
        [Route("calendar/{id}/report")]
        public IActionResult Report(Guid id, [FromBody] string xml = null)
        {
            var calendar = _calendarService.Get(id);
            if (calendar == null) return NotFound();
            if (string.IsNullOrEmpty(xml))
            {
                // Старое поведение: вернуть все события
                var ics = IcsHelper.GenerateIcs(calendar.Events);
                return Content(ics, "text/calendar");
            }
            // Примитивный парсинг типа запроса
            if (xml.Contains("free-busy"))
            {
                // Вернуть free-busy отчет (stub)
                var busy = calendar.Events.Select(e => $"BEGIN:VFREEBUSY\nFREEBUSY:{e.Start:yyyyMMddTHHmmssZ}/{e.End:yyyyMMddTHHmmssZ}\nEND:VFREEBUSY");
                var ics = $"BEGIN:VCALENDAR\n{string.Join("\n", busy)}\nEND:VCALENDAR";
                return Content(ics, "text/calendar");
            }
            if (xml.Contains("calendar-multiget"))
            {
                // Вернуть все события (stub)
                var ics = IcsHelper.GenerateIcs(calendar.Events);
                return Content(ics, "text/calendar");
            }
            if (xml.Contains("calendar-query"))
            {
                // Вернуть все события (stub, без фильтрации)
                var ics = IcsHelper.GenerateIcs(calendar.Events);
                return Content(ics, "text/calendar");
            }
            // По умолчанию вернуть все события
            return Content(IcsHelper.GenerateIcs(calendar.Events), "text/calendar");
        }

        // PROPPATCH (изменение свойств ресурсов)
        [AcceptVerbs("PROPPATCH")]
        [Route("calendar/{id}")]
        public IActionResult PropPatch(Guid id, [FromBody] string xml)
        {
            // Примитивная обработка изменения displayname
            if (!string.IsNullOrEmpty(xml) && xml.Contains("displayname"))
            {
                var start = xml.IndexOf("<displayname>") + 13;
                var end = xml.IndexOf("</displayname>");
                if (start > 0 && end > start)
                {
                    var name = xml.Substring(start, end - start);
                    var cal = _calendarService.Get(id);
                    if (cal != null)
                    {
                        cal.Name = name;
                        _calendarService.Update(id, cal);
                    }
                }
            }
            return Ok();
        }

        // ACL (базовая обработка)
        [AcceptVerbs("ACL")]
        [Route("calendar/{id}/acl")]
        public IActionResult Acl(Guid id, [FromBody] string xml)
        {
            // Примитивный парсинг: если xml содержит <add>, добавляем право, если <get>, возвращаем список
            if (!string.IsNullOrEmpty(xml) && xml.Contains("<add>"))
            {
                // stub: userId и role из xml (в реальности нужен парсинг XML)
                var userId = Guid.NewGuid(); // заменить на парсинг
                var role = "read";
                var entry = new Models.AclEntry { Id = Guid.NewGuid(), CalendarId = id, UserId = userId, Role = role };
                _calendarService.AddAcl(entry);
                return Ok();
            }
            if (!string.IsNullOrEmpty(xml) && xml.Contains("<get>"))
            {
                var acl = _calendarService.GetAcl(id);
                // stub: возвращаем список в виде строки
                var result = string.Join(",", acl.Select(a => $"{a.UserId}:{a.Role}"));
                return Content(result);
            }
            return Ok();
        }

        // OPTIONS (отдача поддерживаемых методов)
        [HttpOptions("")]
        public IActionResult Options()
        {
            Response.Headers.Add("Allow", "OPTIONS, PROPFIND, REPORT, PROPPATCH, PUT, DELETE, MKCALENDAR, GET, POST");
            return Ok();
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