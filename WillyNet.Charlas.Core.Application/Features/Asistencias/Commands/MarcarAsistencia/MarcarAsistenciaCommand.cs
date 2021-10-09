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
        private readonly IRepositoryAsync<CharlaEvento> _repositoryCE;
        private readonly IRepositoryAsync<Evento> _repositoryEvento;

        public MarcarAsistenciaCommandHandler(
            IRepositoryAsync<Asistencia> repositoryAsistencia, IRepositoryAsync<CharlaEvento> repositoryCE,
            IRepositoryAsync<Evento> repositoryEvento
            )
        {
            _repositoryAsistencia = repositoryAsistencia;
            _repositoryCE = repositoryCE;
            _repositoryEvento = repositoryEvento;
        }

        public async Task<Response<Guid>> Handle(MarcarAsistenciaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var asistencia = await _repositoryAsistencia.GetByIdAsync(request.AsistenciaId, cancellationToken);
                if (asistencia == null)
                    throw new ApiException("No registra reserva en alguna charla");
                if (asistencia.Asistio)
                    throw new ApiException("Ya marcó su asistencia");
                if (asistencia.UserAppId != request.UserAppId)
                    throw new ApiException("Usted no está registrado en esta charla");
                
                var charlaEvento = await _repositoryCE.GetByIdAsync(asistencia.CharlaEventoId, cancellationToken);
                var evento = await _repositoryEvento.GetByIdAsync(charlaEvento.EventoId , cancellationToken);                

                if (request.FechaRegistro > evento.FechaFin || request.FechaRegistro < evento.FechaIni)
                    throw new ApiException("La charla a finalizado");

                asistencia.Asistio = true;
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
