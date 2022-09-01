namespace Gambling.Shared.Dtos.Identity;

public class SignUpInputDto
{
    [Required]
    public string? UserName { get; set; }

    [Required]
    public string? Password { get; set; }

    [Required]
    public string? FullName { get; set; }
}
