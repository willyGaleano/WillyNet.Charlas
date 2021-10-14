using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.Exceptions;
using WillyNet.Charlas.Core.Application.Interfaces.Repository;
using WillyNet.Charlas.Core.Application.Wrappers;
using WillyNet.Charlas.Core.Domain.Entities;

namespace WillyNet.Charlas.Core.Application.Features.Charlas.Commands
{
    public class DeshabilitarCharlaCommand : IRequest<Response<bool>>
    {
        public Guid CharlaId { get; set; }
    }

    public class DeshabilitarCharlaCommandHandler : IRequestHandler<DeshabilitarCharlaCommand, Response<bool>>
    {
        private readonly IRepositoryAsync<Charla> _repositoryCharla;

        public DeshabilitarCharlaCommandHandler(IRepositoryAsync<Charla> repositoryCharla)
        {
            _repositoryCharla = repositoryCharla;
        }

        public async Task<Response<bool>> Handle(DeshabilitarCharlaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var charla = await _repositoryCharla.GetByIdAsync(request.CharlaId, cancellationToken);
                if (charla == null)
                    return new Response<bool>(false, "Esta charla no existe");

                charla.DeleteLog = true;
                await _repositoryCharla.UpdateAsync(charla, cancellationToken);

                return new Response<bool>(true, "Charla deshabilitada");
            }
            catch(Exception ex)
            {
                throw new ApiException("Ocurrió un error al deshabilitar la charla.", ex);
            }
        }
    }
}
