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
using Core.Interfaces;

namespace Core.Services
{
    public class AccountService : IAccountService
    {
        protected readonly UserManager<Author> _userManager;
        protected readonly IAuthorService _authorService;
        protected readonly IOptions<JwtOptions> _jwtOptions;
        protected readonly IJwtService _jwtService;
        private RoleManager<IdentityRole> _roleManager;
        private IRepository<RefreshToken> _refreshTokenRepository;
        private IOptions<RolesOptions> _rolesOptions;

        public AccountService(UserManager<Author> userManager, 
            IOptions<JwtOptions> jwtOptions,
            IJwtService jwtService, IRepository<RefreshToken> refreshTokenRepository, IOptions<RolesOptions> rolesOptions, RoleManager<IdentityRole> roleManager, IAuthorService authorService)
        {
            _userManager = userManager;
            _jwtOptions = jwtOptions;
            _jwtService = jwtService;
            _refreshTokenRepository = refreshTokenRepository;
            _rolesOptions = rolesOptions;
            _roleManager = roleManager;
            _authorService = authorService;
        }

        public async Task<AuthorizationDTO> LoginAsync(string email, string password)
        {
            //await _roleManager.CreateAsync(new IdentityRole("User"));
            //await _roleManager.CreateAsync(new IdentityRole("Admin"));
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, password))
            {
                throw new HttpException("Invalid login or password.", System.Net.HttpStatusCode.BadRequest);
            }

            var userRole = await _authorService.GetAuthorRoleAsync(user);
            return await GenerateTokens(user, userRole);
        }

        public async Task<AuthorizationDTO> GenerateTokens(Author author, string userRole)
        {
            var claims = _jwtService.SetClaims(author, userRole);
            var accessToken = _jwtService.CreateToken(claims);
            var refreshToken = await CreateRefreshToken(author.Id);

            var token = _jwtService.CreateToken(claims);

            var tokens = new AuthorizationDTO()
            {
                Token = token,
                RefreshToken = refreshToken.Token
            };

            return tokens;
        }

        private async Task<RefreshToken> CreateRefreshToken(string authorId)
        {
            var refreshToken = _jwtService.CreateRefreshToken();
            var refreshTokenEntity = new RefreshToken()
            {
                Token = refreshToken,
                UserId = authorId
            };
            await _refreshTokenRepository.Insert(refreshTokenEntity);
            return refreshTokenEntity;
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
            string roleName = _rolesOptions.Value.User;
            var userRole = await _roleManager.FindByNameAsync(roleName);

            var roleResult = await _userManager.AddToRoleAsync(user, userRole.Name);
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

        public async Task<AuthorizationDTO> CreateRefreshTokenAsync(string userId)
        {
            var refreshToken = _jwtService.CreateRefreshToken();

            var refreshTokenEntity = new RefreshToken
            {
                Token = refreshToken,
                UserId = userId
            };

            await _refreshTokenRepository.Insert(refreshTokenEntity);

            return new AuthorizationDTO { Token = refreshTokenEntity.Token };
        }

        public async Task<AuthorizationDTO> RefreshTokenAsync(AuthorizationDTO authorizationDTO)
        {
            var refreshToken = await _refreshTokenRepository.Get((el) => el.Token == authorizationDTO.RefreshToken);

            var claims = _jwtService.GetClaimsFromExpiredToken(authorizationDTO.Token);
            var newAccessToken = _jwtService.CreateToken(claims);
            var newRefreshToken = _jwtService.CreateRefreshToken();

            var refreshTokenFirst = refreshToken.First();
            refreshTokenFirst.Token = newRefreshToken;

            _refreshTokenRepository.Update(refreshTokenFirst);

            var tokens = new AuthorizationDTO()
            {
                Token = newAccessToken,
                RefreshToken = newRefreshToken
            };
            return tokens;
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
