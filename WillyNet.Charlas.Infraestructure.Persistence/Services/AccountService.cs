using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.DTOs.User;
using WillyNet.Charlas.Core.Application.Enums;
using WillyNet.Charlas.Core.Application.Exceptions;
using WillyNet.Charlas.Core.Application.Helpers;
using WillyNet.Charlas.Core.Application.Interfaces;
using WillyNet.Charlas.Core.Application.Interfaces.Repository;
using WillyNet.Charlas.Core.Application.Specifications.RefreshTokens;
using WillyNet.Charlas.Core.Application.Wrappers;
using WillyNet.Charlas.Core.Domain.Entities;
using WillyNet.Charlas.Core.Domain.Settings;
using WillyNet.Charlas.Infraestructure.Persistence.Contexts;

namespace WillyNet.Charlas.Infraestructure.Persistence.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<UserApp> _userManager;     
        private readonly SignInManager<UserApp> _signInManager;
        private readonly JWTSettings _jwtSettings;
        private readonly IRepositoryAsync<RefreshToken> _repositoryRefreshToken;
        private readonly DbCharlaContext _dbCharlaContext;
        
        public AccountService(UserManager<UserApp> userManager,            
            IOptions<JWTSettings> jwtSettings,            
            SignInManager<UserApp> signInManager,
            IRepositoryAsync<RefreshToken> repositoryRefreshToken,
            DbCharlaContext dbCharlaContext
            )
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;            
            _signInManager = signInManager;
            _repositoryRefreshToken = repositoryRefreshToken;
            _dbCharlaContext = dbCharlaContext;

        }

        public async Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress)
        {
           
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    throw new ApiException($"No Accounts Registered with {request.Email}.");
                }
                var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
                if (!result.Succeeded)
                {
                    throw new ApiException($"Invalid Credentials for '{request.Email}'.");
                }
                if (!user.EmailConfirmed)
                {
                    throw new ApiException($"Account Not Confirmed for '{request.Email}'.");
                }

                JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);
                AuthenticationResponse response = new()
                {
                    Id = user.Id,
                    JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                    Email = user.Email,
                    UserName = user.UserName
                };
                var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                response.Roles = rolesList.ToList();
                response.IsVerified = user.EmailConfirmed;

                var refrtoq = await _repositoryRefreshToken.ListAsync(new GetAllRefreshTokensSpecification(user.Id));

                if (refrtoq.Count > 0 && refrtoq.Any(a => a.IsActive))
                {
                    var activeRefreshToken = refrtoq.Where(a => a.IsActive == true).FirstOrDefault();
                    response.RefreshToken = activeRefreshToken.Token;
                    response.RefreshTokenExpiration = activeRefreshToken.Expires;
                }
                else
                {
                    var refreshToken = GenerateRefreshToken(ipAddress);
                    response.RefreshToken = refreshToken.Token;
                    response.RefreshTokenExpiration = refreshToken.Expires;

                    refreshToken.UserAppId = user.Id;
                    refreshToken.RefreshTokenId = Guid.NewGuid();
                    await _repositoryRefreshToken.AddAsync(refreshToken);
                }
               
                return new Response<AuthenticationResponse>(response, $"Authenticated {user.UserName}");
           
        }

        public async Task<Response<string>> RegisterAsync(RegisterRequest request, string origin)
        {
            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                throw new ApiException($"Username '{request.UserName}' is already taken.");
            }
            var user = new UserApp
            {
                Email = request.Email,    
                UserName = request.UserName
            };

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail == null)
            {
                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Roles.Basic.ToString());
                    //var verificationUri = await SendVerificationEmail(user, origin);
                    //TODO: Attach Email Service here and configure it via appsettings
                    //await _emailService.SendAsync(new Application.DTOs.Email.EmailRequest() { From = "mail@codewithmukesh.com", To = user.Email, Body = $"Please confirm your account by visiting this URL {verificationUri}", Subject = "Confirm Registration" });
                    return new Response<string>(user.Id, message: "User Registered");
                }
                else
                {
                    throw new ApiException($"{result.Errors.ToArray()[0].Description}");
                }
            }
            else
            {
                throw new ApiException($"Email {request.Email } is already registered.");
            }
        }

        private async Task<JwtSecurityToken> GenerateJWToken(UserApp user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            string ipAddress = IpHelper.GetIpAddress();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id),
                new Claim("ip", ipAddress)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }

        private static string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            // convert random bytes to hex string
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }
        private static RefreshToken GenerateRefreshToken(string ipAddress)
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = DateTime.UtcNow.AddDays(7),
                CreatedToken = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };
        }

        public async Task<Response<AuthenticationResponse>> RefreshTokenAsync(string jwtToken, string ipAddress)
        {
            var user = await _dbCharlaContext.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(r => r.Token == jwtToken));
            if(user == null)            
                return new Response<AuthenticationResponse>("El token no coincide con ningun usuario");

            var refreshToken = await _repositoryRefreshToken.GetBySpecAsync(new GetByTokenRefreshSpecification(jwtToken));
            if (!refreshToken.IsActive)            
                return new Response<AuthenticationResponse>("El token no está activo");

            refreshToken.Revoked = DateTime.Now;
            await _repositoryRefreshToken.UpdateAsync(refreshToken);

            var newRefreshToken = GenerateRefreshToken(ipAddress);
            newRefreshToken.UserAppId = user.Id;
            newRefreshToken.RefreshTokenId = Guid.NewGuid();
            await _repositoryRefreshToken.AddAsync(newRefreshToken);

            //Generando un nuevo jwt
            JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);
            AuthenticationResponse response = new()
            {
                Id = user.Id,
                JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Email = user.Email,
                UserName = user.UserName
            };
            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            response.Roles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;            
            response.RefreshToken = newRefreshToken.Token;
            response.RefreshTokenExpiration = newRefreshToken.Expires;
            return new Response<AuthenticationResponse>(response, $"RefreshToken {user.UserName}");
        }

        public async Task<Response<bool>> RevokeToken(string token)
        {
            var user = _dbCharlaContext.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));
            if (user == null) return new Response<bool>(false);

            var refreshToken = await _repositoryRefreshToken.GetBySpecAsync(new GetByTokenRefreshSpecification(token));
            if (!refreshToken.IsActive) return new Response<bool>(false);

            refreshToken.Revoked = DateTime.Now;
            await _repositoryRefreshToken.UpdateAsync(refreshToken);
            return new Response<bool>(true);
        }

       
    }
}
