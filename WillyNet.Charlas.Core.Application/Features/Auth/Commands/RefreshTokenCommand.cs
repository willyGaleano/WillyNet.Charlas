using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.DTOs.User;
using WillyNet.Charlas.Core.Application.Interfaces;
using WillyNet.Charlas.Core.Application.Wrappers;

namespace WillyNet.Charlas.Core.Application.Features.Auth.Commands
{
    public class RefreshTokenCommand : IRequest<Response<AuthenticationResponse>>
    {
        public string RefreshToken { get; set; }
        public string IpAddress { get; set; }
    }

    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Response<AuthenticationResponse>>
    {
        private readonly IAccountService _accountService;
        public RefreshTokenCommandHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<Response<AuthenticationResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            return await _accountService.RefreshTokenAsync(request.RefreshToken, request.IpAddress);
        }
    }
}
