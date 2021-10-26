using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.DTOs;
using WillyNet.Charlas.Core.Application.Interfaces.Repository;
using WillyNet.Charlas.Core.Application.Wrappers;
using WillyNet.Charlas.Core.Domain.Entities;

namespace WillyNet.Charlas.Core.Application.Features.EstadoAsistencias.Queries.GetEstadoAsistencia
{
    public class GetEstadoAsistenciaQuery : IRequest<Response<EstadoAsistenciaDto>>
    {
        public Guid EstadoAsistenciaId { get; set; }
    }
    public class GetEstadoAsistenciaQueryHandler : IRequestHandler<GetEstadoAsistenciaQuery, Response<EstadoAsistenciaDto>>
    {
        private readonly IRepositoryAsync<EstadoAsistencia> _repositoryEA;
        private readonly IMapper _mapper;

        public GetEstadoAsistenciaQueryHandler(IRepositoryAsync<EstadoAsistencia> repositoryEA, IMapper mapper)
        {
            _repositoryEA = repositoryEA;
            _mapper = mapper;
        }

        public async Task<Response<EstadoAsistenciaDto>> Handle(GetEstadoAsistenciaQuery request, CancellationToken cancellationToken)
        {
            var result = await _repositoryEA.GetByIdAsync(request.EstadoAsistenciaId, cancellationToken);
            var resultDto = _mapper.Map<EstadoAsistenciaDto>(result);

            return new Response<EstadoAsistenciaDto>(resultDto, "Consulta exitosa!");
        }
    }
}
