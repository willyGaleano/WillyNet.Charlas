using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.DTOs;
using WillyNet.Charlas.Core.Application.Interfaces.Repository;
using WillyNet.Charlas.Core.Application.Specifications.Asistencias;
using WillyNet.Charlas.Core.Application.Wrappers;
using WillyNet.Charlas.Core.Domain.Entities;

namespace WillyNet.Charlas.Core.Application.Features.Asistencias.Queries.GetAllAsistencias
{
    public class GetAllAsistenciasQuery : IRequest<PagedResponse<IEnumerable<AsistenciaDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Nombre { get; set; }
        public string AppUserId { get; set; }
    }

    public class GetAllAsistenciasQueryHandler : IRequestHandler<GetAllAsistenciasQuery, PagedResponse<IEnumerable<AsistenciaDto>>>
    {
        private readonly IRepositoryAsync<Asistencia> _repositoryAsistencia;
        private readonly IMapper _mapper;
        public GetAllAsistenciasQueryHandler(IRepositoryAsync<Asistencia> repositoryAsistencia, IMapper mapper)
        {
            _repositoryAsistencia = repositoryAsistencia;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<AsistenciaDto>>> Handle(GetAllAsistenciasQuery request, CancellationToken cancellationToken)
        {
            var asistencias = await _repositoryAsistencia.ListAsync(
                    new GetAllAsistenciasSpecification(request.PageNumber, request.PageSize,
                        request.Nombre, request.AppUserId
                    ),
                    cancellationToken
                );

            var asistenciasDto = _mapper.Map<IEnumerable<AsistenciaDto>>(asistencias);
            var total = await _repositoryAsistencia
                        .CountAsync(new GetAllAsistenciasSpecification(request.PageNumber, request.PageSize,
                        request.Nombre, request.AppUserId
                    ),
                    cancellationToken
                );

            return new PagedResponse<IEnumerable<AsistenciaDto>>
                        (asistenciasDto, request.PageNumber, request.PageSize, total, "¡Consulta exitosa!");
        }
    }
}
