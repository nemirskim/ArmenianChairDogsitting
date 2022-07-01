using Microsoft.AspNetCore.Authorization;

namespace ArmenianChairDogsitting.API
{
    public class AuthorizeByRoleAttribute : AuthorizeAttribute
    {
        public AuthorizeByRoleAttribute(params string[] roles)
        {
            Roles = string.Join(",", roles);
        }
    }

}