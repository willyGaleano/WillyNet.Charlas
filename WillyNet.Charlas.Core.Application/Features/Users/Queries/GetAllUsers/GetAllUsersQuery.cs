using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.DTOs.User;
using WillyNet.Charlas.Core.Application.Interfaces.Repository;
using WillyNet.Charlas.Core.Application.Specifications.Users;
using WillyNet.Charlas.Core.Application.Wrappers;
using WillyNet.Charlas.Core.Domain.Entities;

namespace WillyNet.Charlas.Core.Application.Features.Users.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<PagedResponse<IEnumerable<UserAppDto>>>
    {        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public short Dni { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, PagedResponse<IEnumerable<UserAppDto>>>
    {
        private readonly IRepositoryAsync<UserApp> _repositoryUser;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(IRepositoryAsync<UserApp> repositoryUser, IMapper mapper)
        {
            _repositoryUser = repositoryUser;
            _mapper = mapper;
        }        

        public async Task<PagedResponse<IEnumerable<UserAppDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var result = await _repositoryUser.ListAsync( 
                new GetAllUserSpecification(request.FirstName, request.LastName, request.Dni, 
                    request.PageNumber, request.PageSize),
                cancellationToken);
            var resultDto = _mapper.Map<IEnumerable<UserAppDto>>(result);
            var count = await _repositoryUser.CountAsync(new GetAllUserSpecification(request.FirstName, request.LastName, request.Dni,
                    request.PageNumber, request.PageSize),
                cancellationToken);

            return new PagedResponse<IEnumerable<UserAppDto>>(resultDto, request.PageNumber, request.PageSize, count, "Consulta exitosa!!");
        }
    }
}
