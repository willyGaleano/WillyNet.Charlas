using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.Exceptions;
using WillyNet.Charlas.Core.Application.Interfaces.Repository;
using WillyNet.Charlas.Core.Application.Wrappers;
using WillyNet.Charlas.Core.Domain.Entities;

namespace WillyNet.Charlas.Core.Application.Features.Users.Commands.DisabledUsers
{
    public class DisabledUserCommand : IRequest<Response<bool>>
    {
        public string Id { get; set; }
    }

    public class DisabledUserCommandHandler : IRequestHandler<DisabledUserCommand, Response<bool>>
    {
        private readonly IRepositoryAsync<UserApp> _repositoryUser;
       

        public DisabledUserCommandHandler(IRepositoryAsync<UserApp> repositoryUser)
        {
            _repositoryUser = repositoryUser;            
        }
        public async Task<Response<bool>> Handle(DisabledUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _repositoryUser.GetByIdAsync(request.Id, cancellationToken);
            if (result == null)
                throw new ApiException("Usuario no encontrado.");
            try
            {
                result.DeleteLog = true;
                await _repositoryUser.UpdateAsync(result, cancellationToken);

                return new Response<bool>(true, "Usuario eliminado correctamente.");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
