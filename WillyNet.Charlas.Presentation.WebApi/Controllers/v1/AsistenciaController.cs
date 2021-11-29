using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.Features.Asistencias.Commands.CreateAsistencia;
using WillyNet.Charlas.Core.Application.Features.Asistencias.Commands.DeleteAsistencia;
using WillyNet.Charlas.Core.Application.Features.Asistencias.Commands.MarcarAsistencia;
using WillyNet.Charlas.Core.Application.Features.Asistencias.Queries.GetAllAsistencias;
using WillyNet.Charlas.Core.Application.Features.Asistencias.Queries.GetCantAsistenciaEstado;

namespace WillyNet.Charlas.Presentation.WebApi.Controllers.v1
{
    [Authorize]
    public class AsistenciaController : BaseApiController
    {
        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetAllAsistenciasParameters filters)
        {
            return Ok(await Mediator.Send(
                       new GetAllAsistenciasQuery
                       {
                           PageNumber = filters.PageNumber,
                           PageSize = filters.PageSize,
                           Nombre = filters.Nombre,
                           AppUserId = filters.AppUserId
                       }
                ));
        }

        [HttpGet("GetAllCantEstadoAsync")]
        public async Task<IActionResult> GetAllCantEstadoAsync([FromQuery] string userAppId)
        {
            return Ok(await Mediator.Send(
                       new GetCantAsistenciaEstadoQuery
                       {                           
                           UserAppId = userAppId
                       }
                ));
        }

        [HttpPost("CrearAsistenciaAsync")]
        public async Task<IActionResult> CrearAsistenciaAsync([FromBody] CreateAsistenciaCommand command)
        {
            return Ok(await Mediator.Send(command));
        }


        [HttpPatch("MarcarAsistenciaAsync/{id}")]
        public async Task<IActionResult> MarcarAsistenciaAsync(Guid id , MarcarAsistenciaCommand command)
        {
            command.AsistenciaId = id;
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("RechazarEventoAsync/{id}")]
        public async Task<IActionResult> RechazarEventoAsync(Guid id) 
        {
            return Ok(await Mediator.Send(new DeleteAsistenciaCommand { AsistenciaId = id}));
        }
    }
}
