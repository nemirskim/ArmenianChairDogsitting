using ArmenianChairDogsitting.Business.Exceptions;
using ArmenianChairDogsitting.Business.Hashing;
using ArmenianChairDogsitting.Business.Infrastructure;
using ArmenianChairDogsitting.Business.Interfaces;
using ArmenianChairDogsitting.Data.Enums;
using ArmenianChairDogsitting.Data.Repositories;
using ArmenianChairDogsitting.Data.Repositories.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ArmenianChairDogsitting.Business.Services;

public class AuthService : IAuthService
{
    private readonly IAdminRepository _adminRepository;
    private readonly ISitterRepository _sitterRepository;
    private readonly IClientsRepository _clientRepository;

    public AuthService(IAdminRepository adminRepository, ISitterRepository sitterRepository, IClientsRepository clientRepository)
    {
        _adminRepository = adminRepository;
        _sitterRepository = sitterRepository;
        _clientRepository = clientRepository;
    }

    public string GetToken(User user)
    {
        if (user is null || user.Email is null || user.Role is null)
        {
            throw new DataException("Object or part of it is empty");
        }
        Claim idClaim = new Claim(ClaimTypes.NameIdentifier, user.Id.ToString());
        var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Email), { new Claim(ClaimTypes.Role, user.Role) }, idClaim };

        var jwt = new JwtSecurityToken(
                issuer: AuthOptions.Issuer,
                audience: AuthOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromDays(1)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    public User GetUserForLogin(string email, string password)
    {
        User result = new();

        var admin = _adminRepository.GetAdminByEmail(email);

        if (admin is not null && email == admin.Email && !admin.IsDeleted &&
            PasswordHash.ValidatePassword(password, admin.Password))
        {
            result.Email = email;
            result.Role = Role.Admin.ToString();
        }
        else
        {
            var client = _clientRepository.GetClientByEmail(email);
            var sitter = _sitterRepository.GetSitterByEmail(email);

            if (client == null && sitter == null)
            {
                throw new NotFoundException("User not found");
            }

            dynamic user = client != null ? client : sitter;

            if (user.IsDeleted)
            {
                throw new NotFoundException("User was deleted");
            }

            if (!PasswordHash.ValidatePassword(password, user.Password))
            {
                throw new NotFoundException("The user with such login and password was not found");
            }

            result.Email = user.Email;
            result.Role = client != null ? Role.Client.ToString() : Role.Sitter.ToString();
            result.Id = user.Id;
        }

        return result;
    }
}
