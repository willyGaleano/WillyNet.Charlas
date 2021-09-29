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
    public class GetAllPagedCharlasQuery : IRequest<PagedResponse<IEnumerable<CharlaDto>>>
    {
        public string Nombre { get; set; }
    }

    public class GetAllPagedCharlasQueryHandler : IRequestHandler<GetAllPagedCharlasQuery, PagedResponse<IEnumerable<CharlaDto>>>
    {
        private readonly IRepositoryAsync<Charla> _repositoryCharla;

        public GetAllPagedCharlasQueryHandler(IRepositoryAsync<Charla> repositoryCharla)
        {
            _repositoryCharla = repositoryCharla;
        }
        public Task<PagedResponse<IEnumerable<CharlaDto>>> Handle(GetAllPagedCharlasQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
