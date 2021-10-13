using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.DTOs;
using WillyNet.Charlas.Core.Application.Interfaces.Repository;
using WillyNet.Charlas.Core.Application.Specifications.EstadosEventos;
using WillyNet.Charlas.Core.Application.Specifications.Eventos;
using WillyNet.Charlas.Core.Application.Wrappers;
using WillyNet.Charlas.Core.Domain.Entities;

namespace WillyNet.Charlas.Core.Application.Features.Eventos.Queries.GetAllEventos
{
    public class GetAllEventosQuery : IRequest<PagedResponse<IEnumerable<EventoDto>>>
    {
        public string Nombre { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class GetAllEventosQueryHandler
                        : IRequestHandler<GetAllEventosQuery, PagedResponse<IEnumerable<EventoDto>>>
    {

        private readonly IRepositoryAsync<Evento> _repositoryEvento;
        private readonly IRepositoryAsync<EstadoEvento> _repositoryEstadoEvento;
        private readonly IMapper _mapper;

        public GetAllEventosQueryHandler(IRepositoryAsync<Evento> repositoryEvento,
                IRepositoryAsync<EstadoEvento> repositoryEstadoEvento,
            IMapper mapper)
        {
            _repositoryEvento = repositoryEvento;
            _repositoryEstadoEvento = repositoryEstadoEvento;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<EventoDto>>> Handle(GetAllEventosQuery request, CancellationToken cancellationToken)
        {
            var charEvent = await _repositoryEvento.ListAsync(
                    new GetAllPagedEventosSpecification(request.PageNumber, request.PageSize, request.Nombre),
                cancellationToken) ;

            DateTime fNow = DateTime.Now;
            EstadoEvento estadoEv = null;

            foreach(var item in charEvent)
            {
                if(item.FechaIni <= fNow && fNow <= item.FechaFin)
                {
                    estadoEv = await _repositoryEstadoEvento.GetBySpecAsync(
                        new GetByNameSpecification("En curso"), cancellationToken
                        );
                   
                    item.EstadoEvento = estadoEv;
                }
                if(item.FechaFin < fNow)
                {
                    estadoEv = await _repositoryEstadoEvento.GetBySpecAsync(
                        new GetByNameSpecification("Finalizado"), cancellationToken
                        );
                    
                    item.EstadoEvento = estadoEv;
                }
                if(estadoEv != null)
                    await _repositoryEvento.UpdateAsync(item, cancellationToken);
                estadoEv = null;
            }

            var charEventDto = _mapper.Map<IEnumerable<EventoDto>>(charEvent);
            var total = await _repositoryEvento.CountAsync(
                new GetAllPagedEventosSpecification(request.PageNumber, request.PageSize, request.Nombre),
                cancellationToken
                );

            return new PagedResponse<IEnumerable<EventoDto>>
                (charEventDto, request.PageNumber, request.PageSize, total, "Consulta exitosa!!");
        }
    }
}
