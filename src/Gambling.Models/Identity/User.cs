using Gambling.Models.Common;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gambling.Models.Identity;

public class User : IdentityUser<Guid>, IGamblingEntity
{
    [Required]
    [MaxLength(100)]
    public string? FullName { get; set; }
}
