using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.DTOs;
using WillyNet.Charlas.Core.Application.Interfaces.Repository;
using WillyNet.Charlas.Core.Application.Specifications.EstadosEventos;
using WillyNet.Charlas.Core.Application.Wrappers;
using WillyNet.Charlas.Core.Domain.Entities;

namespace WillyNet.Charlas.Core.Application.Features.EstadoEventos.Queries.GetAllEstadoEventos
{
    public class GetAllEstadoEventoQuery : IRequest<PagedResponse<IEnumerable<EstadoEventoDto>>>
    {
        public string Nombre { get; set; }        
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllEstadoEventoQueryHandler : IRequestHandler<GetAllEstadoEventoQuery, PagedResponse<IEnumerable<EstadoEventoDto>>>
    {
        private readonly IRepositoryAsync<EstadoEvento> _repositoryEV;
        private readonly IMapper _mapper;

        public GetAllEstadoEventoQueryHandler(IRepositoryAsync<EstadoEvento> repositoryEV, IMapper mapper)
        {
            _repositoryEV = repositoryEV;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<EstadoEventoDto>>> Handle(GetAllEstadoEventoQuery request, CancellationToken cancellationToken)
        {
            var result = await _repositoryEV.ListAsync(new GetAllByNameSpecification(request.Nombre, request.PageNumber, request.PageSize));
            var resultMap = _mapper.Map<IEnumerable<EstadoEventoDto>>(result);
            var count = await _repositoryEV.CountAsync(new GetAllByNameSpecification(request.Nombre, request.PageNumber, request.PageSize));
            return new PagedResponse<IEnumerable<EstadoEventoDto>>(resultMap, request.PageNumber, request.PageSize, count, "Consulta exitosa!");
        }
    }
}
