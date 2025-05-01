using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ReactApp1.Server.Models;

public class TokenValidationMiddleware
{
    private readonly RequestDelegate _next;

    public TokenValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, MongoDbService mongoDbService)
    {
        var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        if (!string.IsNullOrEmpty(token))
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jwtToken != null)
            {
                // Log the decoded token
                Console.WriteLine("Decoded JWT Token:");
                Console.WriteLine($"Issuer: {jwtToken.Issuer}");
                Console.WriteLine($"Audiences: {jwtToken.Audiences}");
                Console.WriteLine($"Subject (Email): {jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value}");

                var emailClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;

                // Fetch user from the teams collection based on email
                var teamCollection = mongoDbService.GetTeamCollection();
                var team = await teamCollection.Find(t => t.Email == emailClaim).FirstOrDefaultAsync();

                // Log the retrieved team info
                if (team != null)
                {
                    Console.WriteLine("Retrieved Team Info:");
                    Console.WriteLine($"Name: {team.Name}");
                    Console.WriteLine($"Email: {team.Email}");
                    Console.WriteLine($"Access Level: {team.Access}");

                    // Attach the team info to the HttpContext for later use
                    context.Items["Team"] = team;
                }
                else
                {
                    Console.WriteLine("No team found for the given email.");
                }
            }
            else
            {
                Console.WriteLine("Invalid token.");
            }
        }
        else
        {
            Console.WriteLine("No token provided.");
        }

        await _next(context);
    }
}