﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.Features.Charlas.Commands;
using WillyNet.Charlas.Core.Application.Features.Charlas.Queries;
using WillyNet.Charlas.Core.Application.Features.Charlas.Queries.GetAllCharlas;
using WillyNet.Charlas.Core.Application.Parameters;

namespace WillyNet.Charlas.Presentation.WebApi.Controllers.v1
{
    public class CharlaController : BaseApiController
    {
        [HttpGet("GetAllPaginationCharlaAsync")]
        public async Task<IActionResult> GetAllPaginationCharlaAsync([FromQuery] GetAllCharlaParameters filters)
        {
            return Ok(await Mediator.Send(
                    new GetAllPagedCharlasQuery
                    {
                        Nombre = filters.Nombre,
                        PageNumber = filters.PageNumber,
                        PageSize = filters.PageSize
                    }
                ));
        }

        [HttpGet("GetAllCharlaAsync")]
        public async Task<IActionResult> GetAllCharlaAsync()
        {
            return Ok(await Mediator.Send(new GetAllCharlasQuery()));
        }

        [HttpGet("GetCharlaAsync/{id}")]
        public async Task<IActionResult> GetCharlaAsync(Guid id)
        {
            return Ok(await Mediator.Send(new GetByIdCharlaQuery {CharlaId = id } ));
        }

        [HttpPost("CreateCharlaAsync")]
        public async Task<IActionResult> CreateCharlaAsync([FromForm] CharlaRequest request)
        {
            var command = new CreateCharlaCommand
            {
                NombreCharla = request.NombreCharla,
                DescripcionCharla = request.DescripcionCharla,
                ImgFile = new FileRequest
                {                    
                    Content = request.File.OpenReadStream(),
                    Name = request.File.FileName,
                    ContentType = request.File.ContentType
                }
            };
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("UpdateCharlaAsync/{id}")]
        public async Task<IActionResult> UpdateCharlaAsync(Guid id, [FromForm] CharlaRequest request)
        {
            var command = new UpdateCharlaCommand
            {
                CharlaId = id,
                NombreCharla = request.NombreCharla,
                DescripcionCharla = request.DescripcionCharla,

                ImgFile = request.File != null ? new FileRequest
                {
                    Content = request.File.OpenReadStream(),
                    Name = request.File.FileName,
                    ContentType = request.File.ContentType
                } : null
            };
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("DeleteCharlaAsync/{id}")]
        public async Task<IActionResult> DeleteCharlaAsync(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteCharlaCommand { CharlaId = id}));
        }
    }
}