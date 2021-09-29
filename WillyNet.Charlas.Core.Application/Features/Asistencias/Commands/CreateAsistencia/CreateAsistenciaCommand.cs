using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.Exceptions;
using WillyNet.Charlas.Core.Application.Interfaces.Repository;
using WillyNet.Charlas.Core.Application.Specifications.Asistencias;
using WillyNet.Charlas.Core.Application.Wrappers;
using WillyNet.Charlas.Core.Domain.Entities;

namespace WillyNet.Charlas.Core.Application.Features.Asistencias.Commands.CreateAsistencia
{
    public class CreateAsistenciaCommand : IRequest<Response<Guid>>
    {
        public string UserAppId { get; set; }
        public Guid CharlaEventoId { get; set; }
    }
    public class CreateAsistenciaCommandHandler : IRequestHandler<CreateAsistenciaCommand, Response<Guid>>
    {
        private readonly IRepositoryAsync<Asistencia> _repositoryAsistencia;

        public CreateAsistenciaCommandHandler(IRepositoryAsync<Asistencia> repositoryAsistencia)
        {
            _repositoryAsistencia = repositoryAsistencia;
        }

        public async Task<Response<Guid>> Handle(CreateAsistenciaCommand request, CancellationToken cancellationToken)
        {
            var asistencia = await _repositoryAsistencia
                   .GetBySpecAsync(
                   new GetAsistenciaByUserIdCharlaEveIdEspecification(request.UserAppId, request.CharlaEventoId),
                   cancellationToken
                );
            if (asistencia != null)
                throw new ApiException("Ya se registró en esta charla");

            var id = Guid.NewGuid();
            var newAsistencia = new Asistencia
            {
                AsistenciaId = id,
                CharlaEventoId = request.CharlaEventoId,
                UserAppId = request.UserAppId,
                Asistio = false
            };

            var result = await _repositoryAsistencia.AddAsync(newAsistencia, cancellationToken);

            if (result == null)
                throw new ApiException("No se pudo crear la asistencia");

            return new Response<Guid>(id,"¡Asistencia creada correctamente!");
        }
    }
}
