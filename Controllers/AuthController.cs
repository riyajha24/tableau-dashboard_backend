using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using ReactApp1.Server.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ReactApp1.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMongoCollection<UserModel> _userCollection;
        private const string JwtSecret = "YF6a7tD9q8/ijabdj2J8g+3s1nCzLh8fjU1Zt6aM5k6K9z1lQ="; // Change this to a more secure key in production
        private const int TokenExpirationMinutes = 60; // Token expiration time

        public AuthController(MongoDbService mongoDbService)
        {
            _userCollection = mongoDbService.GetUserCollection();
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(UserModel newUser)
        {
            // Check if a user with the same email already exists
            var existingUser = await _userCollection.Find(u => u.Email == newUser.Email).FirstOrDefaultAsync();
            if (existingUser != null)
            {
                return Conflict(new { Message = "User already exists with this email." });
            }

            // Save the password as is (not hashed)
            await _userCollection.InsertOneAsync(newUser);
            return CreatedAtAction(nameof(Register), new { id = newUser.Id }, newUser);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginModel loginModel)
        {
            var user = await _userCollection.Find(u => u.Email == loginModel.Email).FirstOrDefaultAsync();

            // Use the null-conditional operator to safely access PasswordHash
            if (user == null || loginModel.Password != user?.PasswordHash)
            {
                return Unauthorized();
            }

            // Generate JWT token
            var token = GenerateJwtToken(user);
            return Ok(new { Token = token });
        }

        private string GenerateJwtToken(UserModel user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddMinutes(TokenExpirationMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
