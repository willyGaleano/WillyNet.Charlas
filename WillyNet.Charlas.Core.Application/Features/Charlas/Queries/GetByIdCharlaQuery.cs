using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.DTOs;
using WillyNet.Charlas.Core.Application.Exceptions;
using WillyNet.Charlas.Core.Application.Interfaces.Repository;
using WillyNet.Charlas.Core.Application.Wrappers;
using WillyNet.Charlas.Core.Domain.Entities;

namespace WillyNet.Charlas.Core.Application.Features.Charlas.Queries
{
    public class GetByIdCharlaQuery : IRequest<Response<CharlaDto>>
    {
        public Guid CharlaId { get; set; }
    }
    public class GetByIdCharlaQueryHandler : IRequestHandler<GetByIdCharlaQuery, Response<CharlaDto>>
    {
        private readonly IRepositoryAsync<Charla> _repositoryCharla;
        private readonly IMapper _mapper;

        public GetByIdCharlaQueryHandler(IRepositoryAsync<Charla> repositoryCharla, IMapper mapper)
        {
            _repositoryCharla = repositoryCharla;
            _mapper = mapper;
        }

        public async Task<Response<CharlaDto>> Handle(GetByIdCharlaQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var charla = await _repositoryCharla.GetByIdAsync(request.CharlaId);
                if (charla == null)
                    throw new ApiException("Charla no encontrada");
                var charlaDto = _mapper.Map<CharlaDto>(charla);
                return new Response<CharlaDto>(charlaDto, "¡Consulta exitosa!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
