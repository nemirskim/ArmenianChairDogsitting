using Microsoft.AspNetCore.Mvc;
using ArmenianChairDogsitting.API.Models;
using ArmenianChairDogsitting.Business.Interfaces;
using ArmenianChairDogsitting.API.Extensions;

namespace ArmenianChairDogsitting.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<string> Login([FromBody] UserLoginRequest request)
    {
        var user = _authService.GetUserForLogin(request.Email, request.Password);

        if(user == null ||
            this.GetUserId() != null &&
            this.GetUserId() != user.Id)
        {
            return Unauthorized();
        }

        return _authService.GetToken(user);
    }
}
