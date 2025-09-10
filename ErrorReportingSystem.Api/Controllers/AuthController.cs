using ErrorReportingSystem.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ErrorReportingSystem.Api.Controllers
{
    public record LoginRequest(string Username, string Password);
    public record LoginResponse(string Token);

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokens;
        public AuthController(ITokenService tokens) => _tokens = tokens;

        // Enkla "testanvändare" i minnet
        // (Byt ut mot riktig datakälla senare.)
        private static readonly (string User, string Pass, string Role)[] Users =
        {
            ("admin", "admin123", "Admin"),
            ("user",  "user123",  "User")
        };

        [HttpPost("login")]
        public ActionResult<LoginResponse> Login([FromBody] LoginRequest req)
        {
            var match = Users.FirstOrDefault(u =>
                u.User.Equals(req.Username, StringComparison.OrdinalIgnoreCase) &&
                u.Pass == req.Password);

            if (match == default) return Unauthorized(); // 401

            var token = _tokens.CreateToken(match.User, match.Role);
            return Ok(new LoginResponse(token));
        }
    }
}
