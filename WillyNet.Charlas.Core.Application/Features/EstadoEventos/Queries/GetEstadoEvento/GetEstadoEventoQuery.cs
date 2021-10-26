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
using WillyNet.Charlas.Core.Application.Wrappers;
using WillyNet.Charlas.Core.Domain.Entities;

namespace WillyNet.Charlas.Core.Application.Features.EstadoEventos.Queries.GetEstadoEvento
{
    public class GetEstadoEventoQuery : IRequest<Response<EstadoEventoDto>>
    {
        public Guid EstadoEventoId { get; set; }
    }
    public class GetEstadoEventoQueryHandler : IRequestHandler<GetEstadoEventoQuery, Response<EstadoEventoDto>>
    {
        private readonly IRepositoryAsync<EstadoEvento> _repositoryEV;
        private readonly IMapper _mapper;

        public GetEstadoEventoQueryHandler(IRepositoryAsync<EstadoEvento> repositoryEV, IMapper mapper)
        {
            _repositoryEV = repositoryEV;
            _mapper = mapper;
        }

        public async Task<Response<EstadoEventoDto>> Handle(GetEstadoEventoQuery request, CancellationToken cancellationToken)
        {
            var result = await _repositoryEV.GetByIdAsync(request.EstadoEventoId, cancellationToken);
            var resultDto = _mapper.Map<EstadoEventoDto>(result);

            return new Response<EstadoEventoDto>(resultDto, "Consulta exitosa!");
        }
    }
}
