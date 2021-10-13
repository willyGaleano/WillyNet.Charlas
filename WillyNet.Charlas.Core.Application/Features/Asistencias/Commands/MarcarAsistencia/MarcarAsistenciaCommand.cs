using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.Exceptions;
using WillyNet.Charlas.Core.Application.Interfaces.Repository;
using WillyNet.Charlas.Core.Application.Specifications.EstadosAsistencias;
using WillyNet.Charlas.Core.Application.Wrappers;
using WillyNet.Charlas.Core.Domain.Entities;

namespace WillyNet.Charlas.Core.Application.Features.Asistencias.Commands.MarcarAsistencia
{
    public class MarcarAsistenciaCommand : IRequest<Response<Guid>>
    {
        public Guid AsistenciaId { get; set; }
        public string UserAppId { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
    public class MarcarAsistenciaCommandHandler : IRequestHandler<MarcarAsistenciaCommand, Response<Guid>>
    {
        private readonly IRepositoryAsync<Asistencia> _repositoryAsistencia;
        private readonly IRepositoryAsync<Evento> _repositoryEvento;
        private readonly IRepositoryAsync<EstadoAsistencia> _repositoryEstadoAsist;

        public MarcarAsistenciaCommandHandler(
            IRepositoryAsync<Asistencia> repositoryAsistencia, IRepositoryAsync<Evento> repositoryEvento,
            IRepositoryAsync<EstadoAsistencia> repositoryEstadoAsist
            )
        {
            _repositoryAsistencia = repositoryAsistencia;            
            _repositoryEvento = repositoryEvento;
            _repositoryEstadoAsist = repositoryEstadoAsist;
        }

        public async Task<Response<Guid>> Handle(MarcarAsistenciaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var asistencia = await _repositoryAsistencia.GetByIdAsync(request.AsistenciaId, cancellationToken);
                if (asistencia == null)
                    throw new ApiException("No registra reserva en alguna charla");
               
                var estadoAsist = await _repositoryEstadoAsist.GetByIdAsync(asistencia.EstadoAsistenciaId, cancellationToken);
                if (estadoAsist.Nombre.ToUpper() == "ASISTIÓ")
                    throw new ApiException("Ya marcó su asistencia");
                if (asistencia.UserAppId != request.UserAppId)
                    throw new ApiException("Usted no está registrado en esta charla");
                                
                var evento = await _repositoryEvento.GetByIdAsync(asistencia.EventoId , cancellationToken);                

                if (request.FechaRegistro > evento.FechaFin || request.FechaRegistro < evento.FechaIni)
                    throw new ApiException("La charla a finalizado");

                var estado = await _repositoryEstadoAsist.GetBySpecAsync(
                        new GetByNameSpecification("Asistió"), cancellationToken
                    );

                asistencia.EstadoAsistenciaId = estado.EstadoAsistenciaId;
                await _repositoryAsistencia.UpdateAsync(asistencia, cancellationToken);

                return new Response<Guid>(asistencia.AsistenciaId, "Marcó asistencia correctamente!!");
            }
            catch(Exception ex)
            {
                throw new Exception("Ocurrió un error al marcar asistencia", ex);
            }
        }
    }
}
