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
        private readonly UserManager<Author> _userManager;
        private readonly IOptions<JwtOptions> jwtOptions;
        public AccountService(UserManager<Author> userManager, IOptions<JwtOptions> jwtOptions)
        {
            _userManager = userManager;
            this.jwtOptions = jwtOptions;
        }

        public async Task<AuthorizationDTO> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, password))
            {
                throw new HttpException("Invalid login or password.", System.Net.HttpStatusCode.BadRequest);
            }

            // generate token
            return new AuthorizationDTO
            {
                Token = GenerateToken(email)
            };
        }

        public string GenerateToken(string email)
        {

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, email)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    issuer: jwtOptions.Value.Issuer,
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(jwtOptions.Value.LifeTime),
                    signingCredentials: credentials
                    );

            return new JwtSecurityTokenHandler().WriteToken(token);

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
    }
}
