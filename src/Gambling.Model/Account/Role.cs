﻿using Gambling.Model.Common;
using Microsoft.AspNetCore.Identity;

namespace Gambling.Model.Account;

public class Role : IdentityRole<Guid>, IGamblingEntity
{
}