﻿namespace Gambling.Service.Dtos.Account;

public class AccountInputDto
{
    [Required]
    [JsonIgnore]
    public Guid UserId { get; set; }
}
