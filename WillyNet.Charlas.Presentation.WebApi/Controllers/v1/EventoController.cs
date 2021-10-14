using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.Features.Eventos.Commands;
using WillyNet.Charlas.Core.Application.Features.Eventos.Queries.GetAllEventos;

namespace WillyNet.Charlas.Presentation.WebApi.Controllers.v1
{
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
    }
}
