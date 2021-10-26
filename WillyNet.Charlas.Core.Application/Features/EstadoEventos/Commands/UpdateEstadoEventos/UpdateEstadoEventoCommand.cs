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

namespace WillyNet.Charlas.Core.Application.Features.EstadoEventos.Commands.UpdateEstadoEventos
{
    public class UpdateEstadoEventoCommand : IRequest<Response<Guid>>
    {
        public Guid EstadoEventoId { get; set; }
        public string Nombre { get; set; }
        public string Color { get; set; }
    }
    public class UpdateEstadoEventoCommandHandler : IRequestHandler<UpdateEstadoEventoCommand, Response<Guid>>
    {
        private readonly IRepositoryAsync<EstadoEvento> _repositoryEV;

        public UpdateEstadoEventoCommandHandler(IRepositoryAsync<EstadoEvento> repositoryEV)
        {
            _repositoryEV = repositoryEV;
        }
        public async Task<Response<Guid>> Handle(UpdateEstadoEventoCommand request, CancellationToken cancellationToken)
        {

            var result = await _repositoryEV.GetByIdAsync(request.EstadoEventoId, cancellationToken);
            if (result == null)
                throw new ApiException("Estado evento no encontrado");

            result.Nombre = request.Nombre;
            result.Color = request.Color;
            await _repositoryEV.UpdateAsync(result);

            return new Response<Guid>(result.EstadoEventoId, "Estado Evento actualizado correctamente");
        }
    }
}
