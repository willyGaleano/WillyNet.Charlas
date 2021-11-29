using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.DTOs;
using WillyNet.Charlas.Core.Application.Interfaces.Repository;
using WillyNet.Charlas.Core.Application.Specifications.Asistencias;
using WillyNet.Charlas.Core.Application.Wrappers;
using WillyNet.Charlas.Core.Domain.Entities;

namespace WillyNet.Charlas.Core.Application.Features.Asistencias.Queries.GetCantAsistenciaEstado
{
    public class GetCantAsistenciaEstadoQuery : IRequest<Response<List<CantAsistenciaEstadoDto>>>
    {
        public string UserAppId { get; set; }
    }

    public class GetCantAsistenciaEstadoHandler : IRequestHandler<GetCantAsistenciaEstadoQuery, Response<List<CantAsistenciaEstadoDto>>>
    {
        private readonly IRepositoryAsync<Asistencia> _repositoryAsync;
        private readonly IMapper _mapper;

        public GetCantAsistenciaEstadoHandler(IRepositoryAsync<Asistencia> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }        

        public async Task<Response<List<CantAsistenciaEstadoDto>>> Handle(GetCantAsistenciaEstadoQuery request, CancellationToken cancellationToken)
        {
            var criteria = new GetAllAsistenciasByUserIdSpecification(request.UserAppId);
            var listAsistencias = await _repositoryAsync.ListAsync(criteria, cancellationToken);

            var listFilter = listAsistencias
                    .Where(x => x.Created.Year == DateTime.UtcNow.Year)
                    .OrderBy(x => x.Created)
                    .GroupBy(x => new { x.EstadoAsistencia.Nombre, x.Created.Month })                    
                    .Select(x => new CantAsistenciaEstadoDto { Estado = x.Key.Nombre, Mes = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(x.Key.Month), Cant = x.Count() })
                    .ToList();            

            return new Response<List<CantAsistenciaEstadoDto>>(listFilter, "Consulta exitosa");
                    
        }
    }
}
