namespace Gofbd.Feature
{
    using Gofbd.Core;
    using Gofbd.Dto;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public string CreateAccessToken(UserDto userDto)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration.GetJwtSecretKey()));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expirationTime = DateTime.UtcNow.AddDays(1);
            var claims = new List<Claim>();

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, userDto.UserName));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, userDto.Email));
            claims.Add(new Claim("LoggedInUserId", userDto.UserId.ToString()));

            var token = new JwtSecurityToken(issuer: this._configuration.GetJwtIssuer(),
              audience: this._configuration.GetJwtAudience(),
              claims: claims,
              expires: expirationTime,
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string CreateRefreshToken(UserDto userDto)
        {
            throw new NotImplementedException();
        }
    }
}
