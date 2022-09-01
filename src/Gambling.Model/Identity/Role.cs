using Gambling.Models.Common;
using Microsoft.AspNetCore.Identity;

namespace Gambling.Models.Identity;

public class Role : IdentityRole<Guid>, IGamblingEntity
{
}