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
    public class DeshabilitarEventosCommand : IRequest<Response<bool>>
    {
        public Guid EventoId { get; set; }
    }

    public class DeshabilitarEventosCommandHandler : IRequestHandler<DeshabilitarEventosCommand, Response<bool>>
    {
        private readonly IRepositoryAsync<Evento> _repositoryEvento;

        public DeshabilitarEventosCommandHandler(IRepositoryAsync<Evento> repositoryEvento)
        {
            _repositoryEvento = repositoryEvento;
        }

        public async Task<Response<bool>> Handle(DeshabilitarEventosCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var evento = await _repositoryEvento.GetByIdAsync(request.EventoId, cancellationToken);
                if (evento == null)
                    return new Response<bool>("No existe el evento");

                evento.DeleteLog = true;

                await _repositoryEvento.UpdateAsync(evento, cancellationToken);
                return new Response<bool>(true, "Se eliminó evento");
            }
            catch(Exception ex)
            {
                throw new ApiException(ex.Message, ex);
            }
        }
    }
}
