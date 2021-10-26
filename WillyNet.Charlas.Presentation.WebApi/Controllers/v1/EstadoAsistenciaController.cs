using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.Features.EstadoAsistencias.Commands.CreateEstadoAsistencias;
using WillyNet.Charlas.Core.Application.Features.EstadoAsistencias.Commands.DisabledEstadoAsistencias;
using WillyNet.Charlas.Core.Application.Features.EstadoAsistencias.Commands.UpdateEstadoAsistencias;
using WillyNet.Charlas.Core.Application.Features.EstadoAsistencias.Queries.GetAllEstadoAsistencias;
using WillyNet.Charlas.Core.Application.Features.EstadoAsistencias.Queries.GetEstadoAsistencia;

namespace WillyNet.Charlas.Presentation.WebApi.Controllers.v1
{
    [Authorize]
    public class EstadoAsistenciaController : BaseApiController
    {
        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync([FromQuery]GetAllEstadoAsistenciasParameters parameters)
        {
            var result = await Mediator.Send(new GetAllEstadoAsistenciasCommand
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
            var result = await Mediator.Send(new GetEstadoAsistenciaQuery {EstadoAsistenciaId = id });
            return Ok(result);
        }

        [HttpPost("CreateAsync")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateEstadoAsistenciaCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("UpdateAsync/{id}")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateEstadoAsistenciaCommand command, Guid id)
        {
            command.EstadoAsistenciaId = id;
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPatch("DisabledAsync/{id}")]
        public async Task<IActionResult> DisabledAsync(Guid id)
        {
            var result = await Mediator.Send(new DisabledEstadoAsistenciaCommand { EstadoAsistenciaId = id});
            return Ok(result);
        }
    }
}
