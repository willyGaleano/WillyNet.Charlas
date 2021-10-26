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

namespace WillyNet.Charlas.Core.Application.Features.EstadoEventos.Commands.DisabledEstadoEventos
{
    public class DisabledEstadoEventoCommand : IRequest<Response<bool>>
    {
        public Guid EstadoEventoId { get; set; }
    }
    public class DisabledEstadoEventoCommandHandler : IRequestHandler<DisabledEstadoEventoCommand, Response<bool>>
    {
        private readonly IRepositoryAsync<EstadoEvento> _repositoryEV;

        public DisabledEstadoEventoCommandHandler(IRepositoryAsync<EstadoEvento> repositoryEV)
        {
            _repositoryEV = repositoryEV;
        }
        public async Task<Response<bool>> Handle(DisabledEstadoEventoCommand request, CancellationToken cancellationToken)
        {

            var result = await _repositoryEV.GetByIdAsync(request.EstadoEventoId, cancellationToken);
            if (result == null)
                throw new ApiException("Estado evento no encontrada.");
            try
            {
                result.DeleteLog = true;
                await _repositoryEV.UpdateAsync(result, cancellationToken);

                return new Response<bool>(true, "Estado evento eliminado correctamente.");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
