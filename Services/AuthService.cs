using ECommerceApi.Data;
using ECommerceApi.DTOs;
using ECommerceApi.Interfaces;
using Microsoft.EntityFrameworkCore;
using ECommerceApi.Models;
using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace ECommerceApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(
            ApplicationDbContext context,
            IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto?> RegisterAsync(RegisterDto registerDto)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == registerDto.Email);

            if (existingUser != null)
                return null;

            var user = new User
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                PasswordHash = HashPassword(registerDto.Password),
                Role = registerDto.Role
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return new AuthResponseDto
            {
                Name = user.Name,
                Email = user.Email,
                Token = string.Empty
            };
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginDto loginDto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == loginDto.Email);

            if (user == null)
                return null;

            var hashedPassword = HashPassword(loginDto.Password);

            if (user.PasswordHash != hashedPassword)
                return null;

            return new AuthResponseDto
            {
                Name = user.Name,
                Email = user.Email,
                Token = GenerateToken(user)
            };
        }
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();

            var bytes = Encoding.UTF8.GetBytes(password);

            var hash = sha256.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }
        private string GenerateToken(User user)
        {
            var claims = new[]
{
    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
    new Claim(ClaimTypes.Name, user.Name),
    new Claim(ClaimTypes.Email, user.Email),
    new Claim(ClaimTypes.Role, user.Role)
};

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)
            );

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}