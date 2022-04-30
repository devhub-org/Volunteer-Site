using Core.Entities;
using Core.Exceptions;
using Core.Helpers;
using Core.Interfaces.CustomServices;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Core.Services
{
    public class JwtService : IJwtService
    {
        private readonly IOptions<JwtOptions> jwtOptions;
        private readonly UserManager<Author> userManager;

        public JwtService(IOptions<JwtOptions> jwtOptions)
        {
            this.jwtOptions = jwtOptions;
        }

        //public string CreateRefreshToken()
        //{
        //    var randomNumbers = new byte[32];
        //    using var rng = RandomNumberGenerator.Create();
        //    rng.GetBytes(randomNumbers);
        //    return Convert.ToBase64String(randomNumbers);
        //}

        public string CreateToken(IEnumerable<Claim> claims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: jwtOptions.Value.Issuer,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(jwtOptions.Value.LifeTime),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //public IEnumerable<Claim> GetClaimsFromExpiredToken(string token)
        //{
        //    var tokenValidationParameters = new TokenValidationParameters
        //    {
        //        ValidateIssuer = true,
        //        ValidateAudience = false,
        //        ValidateLifetime = false,
        //        ValidateIssuerSigningKey = true,
        //        ValidIssuer = jwtOptions.Value.Issuer,
        //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.Key)),
        //    };

        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    JwtSecurityToken jwtSecurityToken;

        //    tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        //    jwtSecurityToken = securityToken as JwtSecurityToken;

        //    if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        //        throw new HttpException("Invalid Token!",System.Net.HttpStatusCode.BadRequest);

        //    return jwtSecurityToken.Claims;
        ////}

        public IEnumerable<Claim> SetClaims(Author author)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, author.Id),
                new Claim(ClaimTypes.Name, author.UserName),
            };

            return claims;
        }
    }
}
