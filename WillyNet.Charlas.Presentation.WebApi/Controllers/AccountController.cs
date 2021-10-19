using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.DTOs.User;
using WillyNet.Charlas.Core.Application.Features.Auth.Commands;
using WillyNet.Charlas.Core.Application.Features.Auth.Commands.Register;

namespace WillyNet.Charlas.Presentation.WebApi.Controllers
{
    public class AccountController : BaseApiController
    {
        [HttpPost("Authenticate")]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest request)
        {
            var result = await Mediator.Send(new AuthenticateCommand
            {
                Email = request.Email,
                Password = request.Password,
                IpAddress = GenerateIPAddress()
            });
            setTokenCookie(result.Data.RefreshToken);
            return Ok(result);
        }

        
        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            //Request.Cookies.TryGetValue("X-refreshToken", out var refreshToken);
            var refreshToken = Request.Cookies["X-refreshToken"];
            var result = await Mediator.Send(new RefreshTokenCommand { 
                RefreshToken = refreshToken,
                IpAddress = GenerateIPAddress()
            });
            if (!string.IsNullOrEmpty(result.Data?.RefreshToken))
                setTokenCookie(result.Data.RefreshToken);
            return Ok(result);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            return Ok(await Mediator.Send(new RegisterCommand
            {
                Email = request.Email,
                Password = request.Password,
                ConfirmPassword = request.ConfirmPassword,
                UserName = request.UserName,
                Origin = Request.Headers["origin"]
            }));
        }

        private string GenerateIPAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }

        private void setTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7),
                //SameSite = SameSiteMode.None
            };
            Response.Cookies.Append("X-refreshToken", token, cookieOptions);
        }
    }
}
