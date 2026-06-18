using System.Security.Claims;
using Love4AnimalsAPI.Models;

namespace Love4AnimalsAPI.Interfaces;

public interface IJwtService
{
    string GenerateAccessToken(User user);

    string GenerateRefreshToken();

    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
}