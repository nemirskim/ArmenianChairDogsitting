using Microsoft.AspNetCore.Authorization;

namespace ArmenianChairDogsitting.API.Roles;

public class AuthorizeByRoleAttribute : AuthorizeAttribute
{
    public AuthorizeByRoleAttribute(params string[] roles)
    {
        Roles = String.Join(",", roles);
    }
}
