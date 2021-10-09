using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.Features.CharlasEventos.Commands;
using WillyNet.Charlas.Core.Application.Features.CharlasEventos.Queries.GetAllCharlasEventos;

namespace WillyNet.Charlas.Presentation.WebApi.Controllers.v1
{
    public class CharlaEventoController : BaseApiController
    {
        [HttpPost("CrearCharlaEventoAsync")]
        public async Task<IActionResult> CrearCharlaEventoAsync([FromBody] CreateCharlasEventosCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("GetAllPaginationAsync")]
        public async Task<IActionResult> GetAllPaginationAsync([FromQuery] GetAllCharlasEventosParameters parameters)
        {
            return Ok( await Mediator.Send(new GetAllCharlasEventosQuery { 
                Nombre = parameters.Nombre,
                PageNumber = parameters.PageNumber,
                PageSize = parameters.PageSize
            }));
        }
    }
}
