﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.Features.Asistencias.Commands.CreateAsistencia;
using WillyNet.Charlas.Core.Application.Features.Asistencias.Commands.DeleteAsistencia;
using WillyNet.Charlas.Core.Application.Features.Asistencias.Commands.MarcarAsistencia;
using WillyNet.Charlas.Core.Application.Features.Asistencias.Queries.GetAllAsistencias;

namespace WillyNet.Charlas.Presentation.WebApi.Controllers.v1
{
    public class AsistenciaController : BaseApiController
    {
        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetAllAsistenciasParameters filters)
        {
            return Ok(await Mediator.Send(
                       new GetAllAsistenciasQuery
                       {
                           PageNumber = filters.PageNumber,
                           PageSize = filters.PageSize
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

        [HttpDelete("RechazarCharlaAsync/{id}")]
        public async Task<IActionResult> RechazarCharlaAsync(Guid id) 
        {
            return Ok(await Mediator.Send(new DeleteAsistenciaCommand { AsistenciaId = id}));
        }
    }
}
