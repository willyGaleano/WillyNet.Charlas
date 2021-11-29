using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.Exceptions;
using WillyNet.Charlas.Core.Application.Interfaces;
using WillyNet.Charlas.Core.Application.Interfaces.Repository;
using WillyNet.Charlas.Core.Application.Parameters;
using WillyNet.Charlas.Core.Application.Wrappers;
using WillyNet.Charlas.Core.Domain.Entities;

namespace WillyNet.Charlas.Core.Application.Features.Users.Commands.UpdateUsers
{
    public class UpdateUserCommand : IRequest<Response<string>>
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Dni { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public FileRequest ImgFile { get; set; }
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Response<string>>
    {
        private readonly UserManager<UserApp> _userManager;
        private readonly IRepositoryAsync<UserApp> _repositoryUser;
        private readonly IFileStorageService _fileStorageService;   

        public UpdateUserCommandHandler(UserManager<UserApp> userManager, 
            IRepositoryAsync<UserApp> repositoryUser, IFileStorageService fileStorageService)
        {
            _userManager = userManager;
            _repositoryUser = repositoryUser;
            _fileStorageService = fileStorageService;
        }

        public async Task<Response<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userReq = await _repositoryUser.GetByIdAsync(request.Id, cancellationToken);            
            if(userReq == null)
                throw new ApiException("El usuario no existe.");

            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null && userReq.UserName != userWithSameUserName?.UserName )            
                throw new ApiException($"Username '{request.UserName}' ya existe.");            

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail != null && userReq.Email != userWithSameEmail?.Email)
                throw new ApiException($"Email {request.Email } ya está registrado.");

            if(!string.IsNullOrEmpty(request.Password) && !string.IsNullOrEmpty(request.ConfirmPassword))
            {
                var tokenCode = await _userManager.GeneratePasswordResetTokenAsync(userReq);
                var pass = await _userManager.ResetPasswordAsync(userReq, tokenCode, request.Password);
                if (!pass.Succeeded)
                {
                    throw new ApiException($"{pass.Errors.ToArray()[0].Description}");
                }                
            }

            if (request.ImgFile != null)
            {
                if(!string.IsNullOrEmpty(userReq.ImgUrl))
                {
                    var res = await _fileStorageService.DeleteAsync(userReq.ImgUrl);
                    if (res)
                    {
                        var urlImg = await _fileStorageService.UploadSingleAsync(request.ImgFile, new Guid(request.Id), "users");
                        userReq.ImgUrl = urlImg;
                    }
                    else
                        throw new ApiException("No se pudo eliminar el avatar.");

                }
                else
                {
                    var urlImg = await _fileStorageService.UploadSingleAsync(request.ImgFile, new Guid(request.Id), "users");
                    userReq.ImgUrl = urlImg;
                }
            }

            //var urlImg = await _fileStorageService.UploadSingleAsync(request.ImgFile, Guid.NewGuid(), "users");

            userReq.FirstName = request.FirstName;
            userReq.LastName = request.LastName;
            userReq.Dni = request.Dni;
            userReq.UserName = request.UserName;
            userReq.Email = request.Email;
            



            //try
            //{
            //    await _repositoryUser.UpdateAsync(user, cancellationToken);
            //    return new Response<string>(user.Id, message: "Usuario actualizado correctamente.");
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message, ex);
            //}


            var result = await _userManager.UpdateAsync(userReq);


            if (result.Succeeded)
            {
                return new Response<string>(userReq.Id, message: "Usuario actualizado correctamente.");
            }
            else
            {
                throw new ApiException($"{result.Errors.ToArray()[0].Description}");
            }
        }
    }
}
