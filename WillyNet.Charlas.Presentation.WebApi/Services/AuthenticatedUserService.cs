using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using WillyNet.Charlas.Core.Application.Interfaces;

namespace WillyNet.Charlas.Presentation.WebApi.Services
{
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
        {     
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue("uid");
        }
        public string UserId { get; }
    }
}
