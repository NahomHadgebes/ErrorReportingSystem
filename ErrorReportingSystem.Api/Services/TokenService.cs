// ErrorReportingSystem.Api/Services/TokenService.cs
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ErrorReportingSystem.Api.Services
{
    // Bindas mot "Jwt" i appsettings.json
    public class JwtOptions
    {
        public string Key { get; set; } = "";
        public string Issuer { get; set; } = "";
        public string Audience { get; set; } = "";
        public int ExpireMinutes { get; set; } = 60;
    }

    public interface ITokenService
    {
        string CreateToken(string username, string role);
    }

    public class TokenService : ITokenService
    {
        private readonly JwtOptions _opt;
        public TokenService(IOptions<JwtOptions> opt) => _opt = opt.Value;

        public string CreateToken(string username, string role)
        {
            // 1) Claims i token
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            };

            // 2) Signeringsnyckel
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_opt.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 3) Skapa JWT
            var jwt = new JwtSecurityToken(
                issuer: _opt.Issuer,
                audience: _opt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_opt.ExpireMinutes),
                signingCredentials: creds);

            // 4) Returnera som sträng
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}

