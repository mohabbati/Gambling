using Gambling.Model.Common;
using Microsoft.AspNetCore.Identity;

namespace Gambling.Model.Account;

public class RoleClaim : IdentityRoleClaim<Guid>, IGamblingEntity
{
    public new Guid Id { get; set; }
}