using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ArmenianChairDogsitting.API.Infrastructure;
using ArmenianChairDogsitting.API.Models;

namespace ArmenianChairDogsitting.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : Controller
{
    [HttpPost]
    public string Login([FromBody] UserLoginRequest request)
    {
        if (request == default || request.Email == default) return string.Empty;
        var roleClaim = new Claim(ClaimTypes.Role, (request.Email == "q@qq.qq" ? Role.Admin : Role.Regular).ToString());
        var claims = new List<Claim> { new Claim(ClaimTypes.Name, request.Email), roleClaim };
        var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)), // время действия 2 минуты
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}
