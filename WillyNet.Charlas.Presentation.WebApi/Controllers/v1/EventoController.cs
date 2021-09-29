using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.Features.Eventos.Command;

namespace WillyNet.Charlas.Presentation.WebApi.Controllers.v1
{
    public class EventoController : BaseApiController
    {
        [HttpPost("CreateAsync")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateEventoCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

    }
}
