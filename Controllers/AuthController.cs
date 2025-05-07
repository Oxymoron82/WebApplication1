using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;

namespace ProductsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] TokenRequest request)
        {
            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken, new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new[] { _config["Google:ClientId"] }
                });

                // Успешная валидация
                return Ok(new
                {
                    payload.Email,
                    payload.Name,
                    payload.Picture,
                    payload.Issuer
                });
            }
            catch (InvalidJwtException ex)
            {
                return Unauthorized(new { message = "Invalid Google token", error = ex.Message });
            }
        }

        public class TokenRequest
        {
            public required string IdToken { get; set; }
        }
    }
}
