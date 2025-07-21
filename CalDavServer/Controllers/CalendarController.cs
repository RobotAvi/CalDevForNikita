using Microsoft.AspNetCore.Mvc;
using CalDavServer.Models;
using System;
using System.Collections.Generic;

namespace CalDavServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalendarController : ControllerBase
    {
        // TODO: внедрить сервис и контекст БД через DI

        [HttpGet]
        public ActionResult<IEnumerable<Calendar>> GetAll()
        {
            // Заглушка
            return Ok(new List<Calendar>());
        }

        [HttpGet("{id}")]
        public ActionResult<Calendar> Get(Guid id)
        {
            // Заглушка
            return Ok(new Calendar { Id = id, Name = "Test Calendar" });
        }

        [HttpPost]
        public IActionResult Create([FromBody] Calendar calendar)
        {
            // Заглушка
            return CreatedAtAction(nameof(Get), new { id = calendar.Id }, calendar);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] Calendar calendar)
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