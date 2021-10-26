using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.DTOs.User;
using WillyNet.Charlas.Core.Application.Interfaces.Repository;
using WillyNet.Charlas.Core.Application.Wrappers;
using WillyNet.Charlas.Core.Domain.Entities;

namespace WillyNet.Charlas.Core.Application.Features.Users.Queries.GetUser
{
    public class GetUserQuery : IRequest<Response<UserAppDto>>
    {
        public string Id { get; set; }
    }

    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, Response<UserAppDto>>
    {
        private readonly IRepositoryAsync<UserApp> _repositoryUser;
        private readonly IMapper _mapper;

        public GetUserQueryHandler(IRepositoryAsync<UserApp> repositoryUser, IMapper mapper)
        {
            _repositoryUser = repositoryUser;
            _mapper = mapper;
        }
        public async Task<Response<UserAppDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var result = await _repositoryUser.GetByIdAsync(request.Id);
            var resultDto = _mapper.Map<UserAppDto>(result);

            return new Response<UserAppDto>(resultDto, "Consulta exitosa!!");
        }
    }
}
