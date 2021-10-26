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

namespace WillyNet.Charlas.Core.Application.Features.EstadoAsistencias.Commands.UpdateEstadoAsistencias
{
    public class UpdateEstadoAsistenciaCommand : IRequest<Response<Guid>>
    {
        public Guid EstadoAsistenciaId { get; set; }
        public string Nombre { get; set; }
    }
    public class UpdateEstadoAsistenciaCommandHandler : IRequestHandler<UpdateEstadoAsistenciaCommand, Response<Guid>>
    {
        private readonly IRepositoryAsync<EstadoAsistencia> _repositoryEA;

        public UpdateEstadoAsistenciaCommandHandler(IRepositoryAsync<EstadoAsistencia> repositoryEA)
        {
            _repositoryEA = repositoryEA;
        }
        public async Task<Response<Guid>> Handle(UpdateEstadoAsistenciaCommand request, CancellationToken cancellationToken)
        {

            var result = await _repositoryEA.GetByIdAsync(request.EstadoAsistenciaId, cancellationToken);
            if (result == null)
                throw new ApiException("Estado asistencia no encontrada");

            result.Nombre = request.Nombre;
            await _repositoryEA.UpdateAsync(result);

            return new Response<Guid>(result.EstadoAsistenciaId, "Estado Asistencia actualizada correctamente");
        }
    }
}
