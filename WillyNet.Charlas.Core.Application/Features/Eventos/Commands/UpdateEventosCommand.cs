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

namespace WillyNet.Charlas.Core.Application.Features.Eventos.Commands
{
    public class UpdateEventosCommand : IRequest<Response<Guid>>
    {
        public Guid EventoId { get; set; }
        public DateTime FechaIni { get; set; }
        public short Duracion { get; set; }
        public short Aforo { get; set; }
        public Guid CharlaId { get; set; }
    }

    public class UpdateEventosCommandHandler : IRequestHandler<UpdateEventosCommand, Response<Guid>>
    {
        private readonly IRepositoryAsync<Evento> _repositoryEvento;

        public UpdateEventosCommandHandler(IRepositoryAsync<Evento> repositoryEvento)
        {
            _repositoryEvento = repositoryEvento;
        }

        public async Task<Response<Guid>> Handle(UpdateEventosCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var evento = await _repositoryEvento.GetByIdAsync(request.EventoId, cancellationToken);
                if (evento == null)
                    return new Response<Guid>("El evento no existe");
                evento.FechaIni = request.FechaIni;
                evento.Duracion = request.Duracion;
                evento.Aforo = request.Aforo;
                evento.CharlaId = request.CharlaId;

                await _repositoryEvento.UpdateAsync(evento, cancellationToken);
                return new Response<Guid>(evento.EventoId, "Se actualizó correctamente el evento");
            }
            catch(Exception ex)
            {
                throw new ApiException(ex.Message, ex);
            }

        }
    }

}
