using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.Enums;
using WillyNet.Charlas.Core.Application.Exceptions;
using WillyNet.Charlas.Core.Application.Interfaces;
using WillyNet.Charlas.Core.Application.Parameters;
using WillyNet.Charlas.Core.Application.Wrappers;
using WillyNet.Charlas.Core.Domain.Entities;

namespace WillyNet.Charlas.Core.Application.Features.Users.Commands.CreateUsers
{
    public class CreateUserCommand : IRequest<Response<string>>
    {        
        public string FirstName { get; set; }
        public string LastName { get; set; }        
        public short Dni { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public FileRequest ImgFile { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Response<string>>
    {
        private readonly UserManager<UserApp> _userManager;
        private readonly IFileStorageService _fileStorageService;

        public CreateUserCommandHandler(UserManager<UserApp> userManager, IFileStorageService fileStorageService)
        {
            _userManager = userManager;
            _fileStorageService = fileStorageService;
        }

        public async Task<Response<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                throw new ApiException($"Username '{request.UserName}' ya existe.");
            }

            var urlImg = await _fileStorageService.UploadSingleAsync(request.ImgFile, Guid.NewGuid(), "users");
            var user = new UserApp
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Dni = request.Dni,
                UserName = request.UserName,
                Email = request.Email,
                ImgUrl = urlImg,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail == null)
            {
                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Roles.Basic.ToString());                    
                    return new Response<string>(user.Id, message: "Usuario registrado correctamente.");
                }
                else
                {
                    throw new ApiException($"{result.Errors.ToArray()[0].Description}");
                }
            }
            else
            {
                throw new ApiException($"Email {request.Email } ya está registrado.");
            }
        }
    }
}
