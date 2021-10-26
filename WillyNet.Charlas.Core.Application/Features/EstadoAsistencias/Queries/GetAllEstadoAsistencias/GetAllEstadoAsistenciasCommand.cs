using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.DTOs;
using WillyNet.Charlas.Core.Application.Interfaces.Repository;
using WillyNet.Charlas.Core.Application.Specifications.EstadosAsistencias;
using WillyNet.Charlas.Core.Application.Wrappers;
using WillyNet.Charlas.Core.Domain.Entities;

namespace WillyNet.Charlas.Core.Application.Features.EstadoAsistencias.Queries.GetAllEstadoAsistencias
{
    public class GetAllEstadoAsistenciasCommand : IRequest<PagedResponse<IEnumerable<EstadoAsistenciaDto>>>
    {
        public string Nombre { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllEstadoAsistenciasCommandHandler : IRequestHandler<GetAllEstadoAsistenciasCommand, PagedResponse<IEnumerable<EstadoAsistenciaDto>>>
    {
        private readonly IRepositoryAsync<EstadoAsistencia> _repositoryEA;
        private readonly IMapper _mapper;

        public GetAllEstadoAsistenciasCommandHandler(IRepositoryAsync<EstadoAsistencia> repositoryEA, IMapper mapper)
        {
            _repositoryEA = repositoryEA;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<EstadoAsistenciaDto>>> Handle(GetAllEstadoAsistenciasCommand request, CancellationToken cancellationToken)
        {
            var result = await _repositoryEA.ListAsync(new GetAllByNameSpecification(request.Nombre, request.PageNumber, request.PageSize));
            var resultMap = _mapper.Map<IEnumerable<EstadoAsistenciaDto>>(result);
            var count = await _repositoryEA.CountAsync(new GetAllByNameSpecification(request.Nombre, request.PageNumber, request.PageSize));
            return new PagedResponse<IEnumerable<EstadoAsistenciaDto>>(resultMap,request.PageNumber, request.PageSize, count, "Consulta exitosa!");
        }
    }
}
