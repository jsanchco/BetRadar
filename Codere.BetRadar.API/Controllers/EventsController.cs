namespace Codere.BetRadar.API.Controllers
{
    #region Using

    using Microsoft.AspNetCore.Mvc;
    using Codere.BetRadar.Domain.Services;
    using System;
    using Codere.BetRadar.Domain.Entities;

    #endregion

    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IServiceEvents _serviceEvents;

        public EventsController(IServiceEvents serviceEvents)
        {
            _serviceEvents = serviceEvents ??
                throw new ArgumentNullException(nameof(serviceEvents));
        }

        // GET: api/Events
        [HttpGet]
        public ActionResult<ListEvents> GetEvents()
        {
            var result = _serviceEvents.GetEvents();
            return Ok(result);
        }

        [HttpGet("{eventId}", Name = "GetEvent")]
        public IActionResult GetEvent(int eventId)
        {
            var result = _serviceEvents.GetEvent(eventId);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}