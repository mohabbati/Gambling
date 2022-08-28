using Gambling.Model.Common;
using Microsoft.AspNetCore.Identity;

namespace Gambling.Model.Identity;

public class RoleClaim : IdentityRoleClaim<Guid>, IGamblingEntity
{
    public new Guid Id { get; set; }
}