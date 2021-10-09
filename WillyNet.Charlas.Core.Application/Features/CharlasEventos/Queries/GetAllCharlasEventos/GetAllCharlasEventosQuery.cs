using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.DTOs;
using WillyNet.Charlas.Core.Application.Interfaces.Repository;
using WillyNet.Charlas.Core.Application.Specifications.CharlasEventos;
using WillyNet.Charlas.Core.Application.Wrappers;
using WillyNet.Charlas.Core.Domain.Entities;

namespace WillyNet.Charlas.Core.Application.Features.CharlasEventos.Queries.GetAllCharlasEventos
{
    public class GetAllCharlasEventosQuery : IRequest<PagedResponse<IEnumerable<CharlaEventoTableDto>>>
    {
        public string Nombre { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class GetAllCharlasEventosQueryHandler
                        : IRequestHandler<GetAllCharlasEventosQuery, PagedResponse<IEnumerable<CharlaEventoTableDto>>>
    {

        private readonly IRepositoryAsync<CharlaEvento> _repositoryCE;
        private readonly IMapper _mapper;

        public GetAllCharlasEventosQueryHandler(IRepositoryAsync<CharlaEvento> repositoryCE, IMapper mapper)
        {
            _repositoryCE = repositoryCE;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<CharlaEventoTableDto>>> Handle(GetAllCharlasEventosQuery request, CancellationToken cancellationToken)
        {
            var charEvent = await _repositoryCE.ListAsync(
                    new GetAllPagedChalasEventosSpecification(request.PageNumber, request.PageSize, request.Nombre),
                cancellationToken) ;

            var charEventDto = _mapper.Map<IEnumerable<CharlaEventoTableDto>>(charEvent);
            var total = await _repositoryCE.CountAsync(
                new GetAllPagedChalasEventosSpecification(request.PageNumber, request.PageSize, request.Nombre),
                cancellationToken
                );

            return new PagedResponse<IEnumerable<CharlaEventoTableDto>>
                (charEventDto, request.PageNumber, request.PageSize, total, "Consulta exitosa!!");
        }
    }
}
