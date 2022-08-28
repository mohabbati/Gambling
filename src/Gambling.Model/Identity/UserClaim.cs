using Gambling.Model.Common;
using Microsoft.AspNetCore.Identity;

namespace Gambling.Model.Identity;

public class UserClaim : IdentityUserClaim<Guid>, IGamblingEntity
{
    public new Guid Id { get; set; }
}