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

namespace WillyNet.Charlas.Core.Application.Features.EstadoAsistencias.Commands.CreateEstadoAsistencias
{
    public class CreateEstadoAsistenciaCommand : IRequest<Response<Guid>>
    {
        public string Nombre { get; set; }
    }
    public class CreateEstadoAsistenciaCommandHandler : IRequestHandler<CreateEstadoAsistenciaCommand, Response<Guid>>
    {
        private readonly IRepositoryAsync<EstadoAsistencia> _repositoryEA;

        public CreateEstadoAsistenciaCommandHandler(IRepositoryAsync<EstadoAsistencia> repositoryEA)
        {
            _repositoryEA = repositoryEA;            
        }
        public async Task<Response<Guid>> Handle(CreateEstadoAsistenciaCommand request, CancellationToken cancellationToken)
        {
            var newEstadoAsistencia = new EstadoAsistencia
            {
                EstadoAsistenciaId = Guid.NewGuid(),
                Nombre = request.Nombre
            };

            var result = await _repositoryEA.AddAsync(newEstadoAsistencia, cancellationToken);

            if (result == null)
                throw new ApiException("Error al crear el estado de la asistencia.");

            return new Response<Guid>(result.EstadoAsistenciaId, "Estado Asistencia creado correctamente");
        }
    }
}
