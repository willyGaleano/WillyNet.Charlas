using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.Features.CharlasEventos.Commands;

namespace WillyNet.Charlas.Presentation.WebApi.Controllers.v1
{
    public class CharlaEventoController : BaseApiController
    {
        [HttpPost("CrearCharlaEventoAsync")]
        public async Task<IActionResult> CrearCharlaEventoAsync([FromBody] CreateCharlasEventosCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
