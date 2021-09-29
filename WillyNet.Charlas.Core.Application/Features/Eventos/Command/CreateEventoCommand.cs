using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.Exceptions;
using WillyNet.Charlas.Core.Application.Interfaces.Repository;
using WillyNet.Charlas.Core.Application.Wrappers;
using WillyNet.Charlas.Core.Domain.Entities;

namespace WillyNet.Charlas.Core.Application.Features.Eventos.Command
{
    public class CreateEventoCommand : IRequest<Response<Guid>>
    {        
        public short Aforo { get; set; }
        public DateTime FechaIni { get; set; }
        public short Duracion { get; set; }
        public DateTime FechaFin { get; set; }
        public Guid EstadoId { get; set; }        
    }

    public class CreateEventoCommandHandler : IRequestHandler<CreateEventoCommand, Response<Guid>>
    {
        private readonly IRepositoryAsync<Evento> _repositoryEvento;
        private readonly IMapper _mapper;
        public CreateEventoCommandHandler(IRepositoryAsync<Evento> repositoryEvento, IMapper mapper)
        {
            _repositoryEvento = repositoryEvento;
            _mapper = mapper;
        }

        public async Task<Response<Guid>> Handle(CreateEventoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var newEvento = _mapper.Map<Evento>(request);
                newEvento.EventoId = Guid.NewGuid();
                var result = await _repositoryEvento.AddAsync(newEvento, cancellationToken);

                return new Response<Guid>(newEvento.EventoId, "Se creó correctamente el evento");
            }
            catch(Exception ex)
            {
                throw new ApiException("No se pudo crear el evento", ex);
            }
           
        }
    }
}
