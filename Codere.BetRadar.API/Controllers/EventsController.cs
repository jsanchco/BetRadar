namespace Codere.BetRadar.API.Controllers
{
    #region Using

    using Microsoft.AspNetCore.Mvc;
    using Codere.BetRadar.Domain.Services;
    using System;
    using Codere.BetRadar.Domain.Entities;
    using Serilog;
    using Microsoft.Extensions.Logging;
    using System.Threading.Tasks;

    #endregion

    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IServiceEvents _serviceEvents;
        private readonly ILogger<EventsController> _logger;

        public EventsController(IServiceEvents serviceEvents, ILogger<EventsController> logger)
        {
            _serviceEvents = serviceEvents ??
                throw new ArgumentNullException(nameof(serviceEvents));
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Obtiene el listado de eventos disponibles en el momento de la petición
        /// </summary>
        /// <returns>listado de eventos</returns>
        [HttpGet("GetEvents")]
        public async Task<IActionResult> GetEvents()
        {
            try
            {
                var result = await _serviceEvents.GetEvents();

                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// obtiene un evento filtrado por su id
        /// </summary>
        /// <param name="eventId">id del evento solicitado</param>
        /// <returns>evento solicitado</returns>
        [HttpGet("GetEvent", Name = "GetEvent")]
        public async Task<IActionResult> GetEvent(int eventId)
        {
            try
            {
                var result = await _serviceEvents.GetEvent(eventId);

                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// obtiene el 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetStatuses")]
        public async Task<IActionResult> GetStatusesAsync()
        {
            try
            {
                var result = await _serviceEvents.GetStatuses();

                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// obtiene los streaming de los eventos con el estado solicitado
        /// </summary>
        /// <param name="IdStreamStatus">id del status solicitado</param>
        /// <returns>devuelve la lista de eventos con el estado solicitado</returns>
        [HttpGet("GetEventsByStreamStatus", Name = "GetEventsByStreamStatus")]
        public async Task<IActionResult> GetEventsByStreamStatus(int IdStreamStatus)
        {
            try
            {
                var result = await _serviceEvents.GetEventsByStreamStatus(IdStreamStatus);

                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{stream_id} {stream_type}", Name = "GetStream")]
        public async Task<IActionResult> GetStream(string stream_id, string stream_type)
        {
            try
            {
                var result = await _serviceEvents.GetStream(stream_id, stream_type);

                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(500, ex);
            }
        }

        [HttpGet("GetAllStream", Name = "GetAllStream")]
        public async Task<IActionResult> GetAllStream()
        {
            try
            {
                var result = await _serviceEvents.GetAllStream();

                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(500, ex);
            }
        }

        [HttpGet("GetStreamingInfo", Name = "GetStreamingInfo")]
        public async Task<IActionResult> GetStreamingInfo(string idEvent, bool isMovil)
        {
            try
            {
                var result = await _serviceEvents.GetStreamingInfo(idEvent, isMovil);

                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(500, ex);
            }
        }
    }
}