using ArmenianChairDogsitting.Data.Enums;
using Microsoft.AspNetCore.Authorization;

namespace ArmenianChairDogsitting.API;

public class AuthorizeByRoleAttribute : AuthorizeAttribute
{
    public AuthorizeByRoleAttribute(params Role[] roles)
    {
        Roles = string.Join(",", roles);
        Roles += $",{Role.Admin}";
    }
}
