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

namespace WillyNet.Charlas.Core.Application.Features.EstadoEventos.Commands.CreateEstadoEventos
{
    public class CreateEstadoEventoCommand : IRequest<Response<Guid>>
    {
        public string Nombre { get; set; }
        public string Color { get; set; }
    }
    public class CreateEstadoEventoCommandHandler : IRequestHandler<CreateEstadoEventoCommand, Response<Guid>>
    {
        private readonly IRepositoryAsync<EstadoEvento> _repositoryEV;

        public CreateEstadoEventoCommandHandler(IRepositoryAsync<EstadoEvento> repositoryEV)
        {
            _repositoryEV = repositoryEV;
        }
        public async Task<Response<Guid>> Handle(CreateEstadoEventoCommand request, CancellationToken cancellationToken)
        {
            var newEstadoEvento = new EstadoEvento
            {
                EstadoEventoId = Guid.NewGuid(),
                Nombre = request.Nombre,
                Color = request.Color
            };

            var result = await _repositoryEV.AddAsync(newEstadoEvento, cancellationToken);

            if (result == null)
                throw new ApiException("Error al crear el estado del evento.");

            return new Response<Guid>(result.EstadoEventoId, "Estado Evento creado correctamente");
        }
    }
}
