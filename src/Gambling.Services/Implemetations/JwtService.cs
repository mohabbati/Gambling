using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Gambling.Models.Identity;
using Gambling.Shared.Dtos.Identity;
using Gambling.Models;

namespace Gambling.Services.Implemetations;

public class JwtService : IJwtService
{
    private readonly AppSettings _appSettings;
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;

    public JwtService(IOptions<AppSettings> appSettings, SignInManager<User> signInManager, UserManager<User> userManager)
    {
        _appSettings = appSettings.Value;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<SignInOutputDto> GenerateTokenAsync(SignInInputDto input, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(input.UserName);

        var secretKey = Encoding.UTF8.GetBytes(_appSettings.JwtSettings.SecretKey);
        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

        var claims = (await _signInManager.ClaimsFactory.CreateAsync(user)).Claims;

        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

        var securityToken = jwtSecurityTokenHandler
            .CreateJwtSecurityToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.JwtSettings.Issuer,
                Audience = _appSettings.JwtSettings.Audience,
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now.AddMinutes(_appSettings.JwtSettings.NotBeforeMinutes),
                Expires = DateTime.Now.AddMinutes(_appSettings.JwtSettings.ExpirationMinutes),
                SigningCredentials = signingCredentials,
                Subject = new ClaimsIdentity(claims)
            });

        var signInOutputDto = new SignInOutputDto
        {
            AccessToken = jwtSecurityTokenHandler.WriteToken(securityToken),
            ExpiresIn = (long)TimeSpan.FromMinutes(_appSettings.JwtSettings.ExpirationMinutes).TotalSeconds
        };

        return signInOutputDto;
    }
}
