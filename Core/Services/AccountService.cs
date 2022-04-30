using AutoMapper;
using Core.DTO;
using Core.Entities;
using Core.Exceptions;
using Core.Helpers;
using Core.Interfaces.CustomServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class AccountService : IAccountService
    {
        protected readonly UserManager<Author> _userManager;
        protected readonly IOptions<JwtOptions> _jwtOptions;
        protected readonly IJwtService _jwtService;

        public AccountService(UserManager<Author> userManager, 
            IOptions<JwtOptions> jwtOptions,
            IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtOptions = jwtOptions;
            _jwtService = jwtService;
        }

        public async Task<AuthorizationDTO> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, password))
            {
                throw new HttpException("Invalid login or password.", System.Net.HttpStatusCode.BadRequest);
            }

            return await GenerateToken(user);
        }

        public async Task<AuthorizationDTO> GenerateToken(Author author)
        {
            var claims = _jwtService.SetClaims(author);

            var token = _jwtService.CreateToken(claims);

            var tokens = new AuthorizationDTO()
            {
                Token = token,
            };

            return tokens;
        }

        public async Task RegisterAsync(RegisterUserDTO data)
        {
            var user = new Author()
            {
                UserName = data.Email,
                Email = data.Email,
                Name = data.Name,
                Surname = data.Surname
            };
            var result = await _userManager.CreateAsync(user, data.Password);

            if (!result.Succeeded)
            {
                StringBuilder messageBuilder = new StringBuilder();

                foreach (var error in result.Errors)
                {
                    messageBuilder.AppendLine(error.Description);
                }

                throw new HttpException(messageBuilder.ToString(), System.Net.HttpStatusCode.BadRequest);
            }
        }

        public Task<AuthorizationDTO> RefreshTokenAsync(AuthorizationDTO userTokensDTO)
        {
            throw new NotImplementedException();
        }

        public Task LogoutAsync(AuthorizationDTO userTokensDTO)
        {
            throw new NotImplementedException();
        }

        public Task SentResetPasswordTokenAsync(string userEmail)
        {
            throw new NotImplementedException();
        }
    }
}
