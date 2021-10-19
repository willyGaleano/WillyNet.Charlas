using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.Features.Eventos.Commands;
using WillyNet.Charlas.Core.Application.Features.Eventos.Queries.GetAllEventos;

namespace WillyNet.Charlas.Presentation.WebApi.Controllers.v1
{
    [Authorize]
    public class EventoController : BaseApiController
    {
        [HttpPost("CrearEventoAsync")]
        public async Task<IActionResult> CrearEventoAsync([FromBody] CreateEventosCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("GetAllPaginationAsync")]
        public async Task<IActionResult> GetAllPaginationAsync([FromQuery] GetAllEventosParameters parameters)
        {
            return Ok( await Mediator.Send(new GetAllEventosQuery { 
                Nombre = parameters.Nombre,
                IsAdmin = parameters.IsAdmin,
                PageNumber = parameters.PageNumber,
                PageSize = parameters.PageSize
            }));
        }

        [HttpPut("UpdateEventoAsync/{id}")]
        public async Task<IActionResult> UpdateEventoAsync(Guid id, [FromBody] UpdateEventosCommand command)
        {
            command.EventoId = id;
            return Ok(await Mediator.Send(command));
        }

        [HttpPatch("DeleteLogEventoAsync/{id}")]
        public async Task<IActionResult> DeleteLogEventoAsync(Guid id)
        {            
            return Ok(await Mediator.Send(new DeshabilitarEventosCommand {EventoId = id }));
        }
    }
}
