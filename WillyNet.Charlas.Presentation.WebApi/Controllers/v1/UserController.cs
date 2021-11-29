using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.Features.Users.Commands.CreateUsers;
using WillyNet.Charlas.Core.Application.Features.Users.Commands.DisabledUsers;
using WillyNet.Charlas.Core.Application.Features.Users.Commands.UpdateUsers;
using WillyNet.Charlas.Core.Application.Features.Users.Queries.GetAllUsers;
using WillyNet.Charlas.Core.Application.Features.Users.Queries.GetUser;
using WillyNet.Charlas.Core.Application.Parameters;

namespace WillyNet.Charlas.Presentation.WebApi.Controllers.v1
{
    [Authorize]
    public class UserController : BaseApiController
    {
        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetAllUsersParameters parameters)
        {
            var result = await Mediator.Send(new GetAllUsersQuery
            {
                FirstName = parameters.FirstName,
                LastName = parameters.LastName,
                Dni = parameters.Dni,
                PageNumber = parameters.PageNumber,
                PageSize = parameters.PageSize                
            });

            return Ok(result);
        }

        [HttpGet("GetAsync/{id}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            var result = await Mediator.Send(new GetUserQuery { Id = id });
            return Ok(result);
        }

        [HttpPost("CreateAsync")]
        public async Task<IActionResult> CreateAsync([FromForm] UserRequest request)
        {
            var command = new CreateUserCommand
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Dni = request.Dni,
                UserName = request.UserName,
                Email = request.Email,
                Password = request.Password,
                ConfirmPassword = request.ConfirmPassword,
                ImgFile = request.File != null ? new FileRequest
                {
                    Content = request.File.OpenReadStream(),
                    Name = request.File.FileName,
                    ContentType = request.File.ContentType
                } : null

            };

            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("UpdateAsync/{id}")]
        public async Task<IActionResult> UpdateAsync([FromForm] UserRequest request, string id)
        {

            var command = new UpdateUserCommand
            {
                Id = id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Dni = request.Dni,
                UserName = request.UserName,
                Email = request.Email,
                Password = request.Password,
                ConfirmPassword = request.ConfirmPassword,
                ImgFile = request.File != null ? new FileRequest
                {
                    Content = request.File.OpenReadStream(),
                    Name = request.File.FileName,
                    ContentType = request.File.ContentType
                } : null

            };

            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPatch("DisabledAsync/{id}")]
        public async Task<IActionResult> DisabledAsync(string id)
        {
            var result = await Mediator.Send(new DisabledUserCommand { Id = id });
            return Ok(result);
        }
    }
}
