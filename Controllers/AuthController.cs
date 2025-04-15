using Microsoft.AspNetCore.Mvc;
using HotelBookingAPI.Application.Services;
using HotelBookingAPI.Domain;
using HotelBookingAPI.Presentation;
using System.Text;
using HotelBookingAPI.Application;
using System.Security.Cryptography;
using AutoMapper;

namespace HotelBookingAPI.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly IMapper _mapper;
        public AuthController(AuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }


        [HttpPost("login")]
        [Consumes("application/json")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userDto)
        {
            var user = await _authService.AuthenticateAsync(userDto.Email, userDto.Password);

            if (user == null || string.IsNullOrEmpty(user.Token))
            {
                return Unauthorized(new { message = "Usuário ou senha inválidos" });
            }

            return Ok(new AuthResponseDto
            {
                Token = user.Token,
                Email = user.Email,
                Username = user.Username
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserLoginDto userDto)
        {

            var user = new User
            {
                Username = userDto.Username,
                Email = userDto.Email,
                PasswordHash = ComputeHash(userDto.Password)
            };

            var result = await _authService.RegisterUserAsync(user);
            if (!result)
            {
                return BadRequest(new { message = "Erro ao registrar o usuário" });
            }

            return Ok(new { message = "Usuário registrado com sucesso" });
        }

        private string ComputeHash(string input)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
