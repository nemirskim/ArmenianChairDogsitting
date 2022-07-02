using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ArmenianChairDogsitting.API.Infrastructure;
using ArmenianChairDogsitting.API.Models;
using ArmenianChairDogsitting.API.Roles;

namespace ArmenianChairDogsitting.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : Controller
{
    [HttpPost]
    public string Login([FromBody] UserLoginRequest request)
    {
        if (request == default || request.Email == default) return string.Empty;
        var roleClaim = new Claim(ClaimTypes.Role, Role.Client.ToString());

        switch (request.Email)
        {
            case "dogsitter@dd.d":
                roleClaim = new Claim(ClaimTypes.Role, Role.Sitter.ToString());
                break;
            case "manager@mm.m":
                roleClaim = new Claim(ClaimTypes.Role, Role.Admin.ToString());
                break;
        }

        var claims = new List<Claim> { new Claim(ClaimTypes.Name, request.Email), roleClaim };
        
        var jwt = new JwtSecurityToken(
                issuer: AuthOptions.Issuer,
                audience: AuthOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromHours(2)), // время действия 2 часа
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}
