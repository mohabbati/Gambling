﻿namespace Gambling.Shared.Dtos.Identity;

public class SignInOutputDto
{
    public string? AccessToken { get; set; }

    public long ExpiresIn { get; set; }
}
