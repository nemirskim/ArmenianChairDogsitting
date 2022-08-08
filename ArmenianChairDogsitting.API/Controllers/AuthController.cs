﻿using Microsoft.AspNetCore.Mvc;
using ArmenianChairDogsitting.API.Models;
using ArmenianChairDogsitting.Business.Interfaces;

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
    public string Login([FromBody] UserLoginRequest request)
    {
        var user = _authService.GetUserForLogin(request.Email, request.Password);

        return _authService.GetToken(user);
    }
}
