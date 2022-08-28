﻿using Gambling.Model.Common;
using Microsoft.AspNetCore.Identity;

namespace Gambling.Model.Identity;

public class Role : IdentityRole<Guid>, IGamblingEntity
{
}