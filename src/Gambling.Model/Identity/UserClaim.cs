using Gambling.Models.Common;
using Microsoft.AspNetCore.Identity;

namespace Gambling.Models.Identity;

public class UserClaim : IdentityUserClaim<Guid>, IGamblingEntity
{
    public new Guid Id { get; set; }
}