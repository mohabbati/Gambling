﻿namespace Gambling.Shared.Dtos.Identity;

public class SignInInputDto
{
    [Required]
    public string? UserName { get; set; }

    [Required]
    public string? Password { get; set; }
}
