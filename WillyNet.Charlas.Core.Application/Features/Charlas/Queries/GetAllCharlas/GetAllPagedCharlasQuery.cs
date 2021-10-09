using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.DTOs;
using WillyNet.Charlas.Core.Application.Interfaces.Repository;
using WillyNet.Charlas.Core.Application.Specifications.Charlas;
using WillyNet.Charlas.Core.Application.Wrappers;
using WillyNet.Charlas.Core.Domain.Entities;

namespace WillyNet.Charlas.Core.Application.Features.Charlas.Queries.GetAllCharlas
{
    public class GetAllPagedCharlasQuery : IRequest<PagedResponse<IEnumerable<CharlaDto>>>
    {
        public string Nombre { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class GetAllPagedCharlasQueryHandler : IRequestHandler<GetAllPagedCharlasQuery, PagedResponse<IEnumerable<CharlaDto>>>
    {
        private readonly IRepositoryAsync<Charla> _repositoryCharla;
        private readonly IMapper _mapper;

        public GetAllPagedCharlasQueryHandler(IRepositoryAsync<Charla> repositoryCharla, IMapper mapper)
        {
            _repositoryCharla = repositoryCharla;
            _mapper = mapper;
        }
        public async Task<PagedResponse<IEnumerable<CharlaDto>>> Handle(GetAllPagedCharlasQuery request, CancellationToken cancellationToken)
        {
           try
            {
                var charlasList = await _repositoryCharla.ListAsync(
                        new GetAllCharlaSpecification(request.PageNumber, request.PageSize, request.Nombre),
                        cancellationToken
                    );
                var charlasDtoList = _mapper.Map<IEnumerable<CharlaDto>>(charlasList);
                var total = await _repositoryCharla.CountAsync(
                     new GetAllCharlaSpecification(request.PageNumber, request.PageSize, request.Nombre),
                        cancellationToken
                    );

                return new PagedResponse<IEnumerable<CharlaDto>>(
                    charlasDtoList, request.PageNumber, request.PageSize, total, "¡Consulta exitosa!"
                    );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
