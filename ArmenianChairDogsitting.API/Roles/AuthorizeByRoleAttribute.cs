using Microsoft.AspNetCore.Authorization;

namespace ArmenianChairDogsitting.API.Roles;

public class AuthorizeByRoleAttribute : AuthorizeAttribute
{
    public AuthorizeByRoleAttribute(params Role[] roles)
    {
        Roles = String.Join(",", roles);
        Roles += $",{Role.Admin}";
    }
}
