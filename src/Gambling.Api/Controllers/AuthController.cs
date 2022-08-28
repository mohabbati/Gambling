﻿using Gambling.Service;
using Gambling.Service.Dtos.Identity;

namespace Gambling.Api.Controllers;

[AllowAnonymous]
public class AuthController : GamblingControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> SignUp(SignUpInputDto input, CancellationToken cancellationToken)
    {
        var result = await _authService.SignUp(input, cancellationToken);

        return result.ToOk(c => c);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> SignIn(SignInInputDto input, CancellationToken cancellationToken)
    {
        var result = await _authService.SignIn(input, cancellationToken);

        return result.ToOk(c => c);
    }
}
