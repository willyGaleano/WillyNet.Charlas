using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.DTOs.User;
using WillyNet.Charlas.Core.Application.Wrappers;

namespace WillyNet.Charlas.Core.Application.Interfaces
{
    public interface IAccountService
    {
        Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress);
        Task<Response<string>> RegisterAsync(RegisterRequest request, string origin);
        Task<Response<AuthenticationResponse>> RefreshTokenAsync(string jwtToken, string ipAddress);
        Task<Response<bool>> RevokeToken(string token);
        //Task<Response<string>> ConfirmEmailAsync(string userId, string code);
        //Task ForgotPassword(ForgotPasswordRequest model, string origin);
        //Task<Response<string>> ResetPassword(ResetPasswordRequest model);
    }
}
