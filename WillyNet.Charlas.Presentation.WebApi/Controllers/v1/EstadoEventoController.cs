using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.Features.EstadoEventos.Commands.CreateEstadoEventos;
using WillyNet.Charlas.Core.Application.Features.EstadoEventos.Commands.DisabledEstadoEventos;
using WillyNet.Charlas.Core.Application.Features.EstadoEventos.Commands.UpdateEstadoEventos;
using WillyNet.Charlas.Core.Application.Features.EstadoEventos.Queries.GetAllEstadoEventos;
using WillyNet.Charlas.Core.Application.Features.EstadoEventos.Queries.GetEstadoEvento;

namespace WillyNet.Charlas.Presentation.WebApi.Controllers.v1
{
    [Authorize]
    public class EstadoEventoController : BaseApiController
    {
        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetAllEstadoEventoParameters parameters)
        {
            var result = await Mediator.Send(new GetAllEstadoEventoQuery
            {
                Nombre = parameters.Nombre,
                PageNumber = parameters.PageNumber,
                PageSize = parameters.PageSize
            });

            return Ok(result);
        }

        [HttpGet("GetAsync/{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var result = await Mediator.Send(new GetEstadoEventoQuery { EstadoEventoId = id });
            return Ok(result);
        }

        [HttpPost("CreateAsync")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateEstadoEventoCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("UpdateAsync/{id}")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateEstadoEventoCommand command, Guid id)
        {
            command.EstadoEventoId = id;
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPatch("DisabledAsync/{id}")]
        public async Task<IActionResult> DisabledAsync(Guid id)
        {
            var result = await Mediator.Send(new DisabledEstadoEventoCommand { EstadoEventoId = id });
            return Ok(result);
        }
    }
}
