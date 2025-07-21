using Microsoft.AspNetCore.Mvc;
using CalDavServer.Models;
using System;
using System.Collections.Generic;

namespace CalDavServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        // TODO: внедрить сервис и контекст БД через DI

        [HttpGet]
        public ActionResult<IEnumerable<Event>> GetAll()
        {
            // Заглушка
            return Ok(new List<Event>());
        }

        [HttpGet("{id}")]
        public ActionResult<Event> Get(Guid id)
        {
            // Заглушка
            return Ok(new Event { Id = id, Summary = "Test Event" });
        }

        [HttpPost]
        public IActionResult Create([FromBody] Event ev)
        {
            // Заглушка
            return CreatedAtAction(nameof(Get), new { id = ev.Id }, ev);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] Event ev)
        {
            // Заглушка
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            // Заглушка
            return NoContent();
        }
    }
}