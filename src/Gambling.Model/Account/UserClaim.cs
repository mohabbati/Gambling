using Gambling.Model.Common;
using Microsoft.AspNetCore.Identity;

namespace Gambling.Model.Account;

public class UserClaim : IdentityUserClaim<Guid>, IGamblingEntity
{
    public new Guid Id { get; set; }
}