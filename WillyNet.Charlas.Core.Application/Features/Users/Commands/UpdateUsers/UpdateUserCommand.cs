using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.Exceptions;
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
        public short Dni { get; set; }
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

        public UpdateUserCommandHandler(UserManager<UserApp> userManager, IRepositoryAsync<UserApp> repositoryUser)
        {
            _userManager = userManager;
            _repositoryUser = repositoryUser;
        }

        public async Task<Response<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userReq = await _repositoryUser.GetByIdAsync(request.Id, cancellationToken);
            if(userReq == null)
                throw new ApiException("El usuario no existe.");

            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);

            if (userWithSameUserName != null && userReq.UserName != userWithSameUserName?.UserName )
            {
                throw new ApiException($"Username '{request.UserName}' ya existe.");
            }
            var user = new UserApp
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Dni = request.Dni,
                UserName = request.UserName,
                Email = request.Email
            };

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if(userWithSameEmail != null && userReq.Email != userWithSameEmail?.Email )
                throw new ApiException($"Email {request.Email } ya está registrado.");

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                //await _userManager.AddToRoleAsync(user, Roles.Basic.ToString());
                return new Response<string>(user.Id, message: "Usuario actualizado correctamente.");
            }
            else
            {
                throw new ApiException($"{result.Errors.ToArray()[0].Description}");
            }
        }
    }
}
