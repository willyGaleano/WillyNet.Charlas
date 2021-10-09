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

namespace WillyNet.Charlas.Core.Application.Features.Charlas.Queries.GetAllCharlas
{
    public class GetAllCharlasQuery : IRequest<Response<IEnumerable<CharlaDto>>>
    {
    }
    public class GetAllCharlasQueryHandler : IRequestHandler<GetAllCharlasQuery, Response<IEnumerable<CharlaDto>>>
    {
        private readonly IRepositoryAsync<Charla> _repositoryCharla;
        private readonly IMapper _mapper;

        public GetAllCharlasQueryHandler(IRepositoryAsync<Charla> repositoryCharla, IMapper mapper)
        {
            _repositoryCharla = repositoryCharla;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<CharlaDto>>> Handle(GetAllCharlasQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var charlaList = await _repositoryCharla.ListAsync(cancellationToken);
                var charlaDtoList = _mapper.Map<IEnumerable<CharlaDto>>(charlaList);
                return new Response<IEnumerable<CharlaDto>>(charlaDtoList,"¡Consulta exitosa!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }
    }
}
