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

namespace WillyNet.Charlas.Core.Application.Features.EstadoAsistencias.Commands.DisabledEstadoAsistencias
{
    public class DisabledEstadoAsistenciaCommand : IRequest<Response<bool>>
    {        
        public Guid EstadoAsistenciaId { get; set; }
    }
    public class DisabledEstadoAsistenciaCommandHandler : IRequestHandler<DisabledEstadoAsistenciaCommand, Response<bool>>
    {
        private readonly IRepositoryAsync<EstadoAsistencia> _repositoryEA;

        public DisabledEstadoAsistenciaCommandHandler(IRepositoryAsync<EstadoAsistencia> repositoryEA)
        {
            _repositoryEA = repositoryEA;
        }
        public async Task<Response<bool>> Handle(DisabledEstadoAsistenciaCommand request, CancellationToken cancellationToken)
        {

            var result = await _repositoryEA.GetByIdAsync(request.EstadoAsistenciaId, cancellationToken);
            if (result == null)
                throw new ApiException("Estado asistencia no encontrada");
            try
            {
                result.DeleteLog = true;
                await _repositoryEA.UpdateAsync(result, cancellationToken);

                return new Response<bool>(true, "Estado Asistencia eliminado correctamente");
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
