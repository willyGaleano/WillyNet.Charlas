using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.Interfaces.Repository;
using WillyNet.Charlas.Core.Application.Wrappers;
using WillyNet.Charlas.Core.Domain.Entities;

namespace WillyNet.Charlas.Core.Application.Features.Charlas.Commands
{
    public class DeleteCharlaCommand : IRequest<Response<bool>>
    {
        public Guid CharlaId { get; set; }
    }

    public class DeleteCharlaCommandHandler : IRequestHandler<DeleteCharlaCommand, Response<bool>>
    {
        private readonly IRepositoryAsync<Charla> _repositoryCharla;

        public DeleteCharlaCommandHandler(IRepositoryAsync<Charla> repositoryCharla)
        {
            _repositoryCharla = repositoryCharla;
        }
        public async Task<Response<bool>> Handle(DeleteCharlaCommand request, CancellationToken cancellationToken)
        {
            try
            {
               await _repositoryCharla.DeleteAsync(
                        await _repositoryCharla.GetByIdAsync(request.CharlaId, cancellationToken),
                        cancellationToken
               );

                return new Response<bool>(true, "Charla eliminada correctamente");
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
