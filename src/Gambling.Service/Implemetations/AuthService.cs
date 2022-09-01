using Gambling.Model.Identity;
using Gambling.Shared.Dtos.Identity;
using Microsoft.AspNetCore.Identity;

namespace Gambling.Service.Implemetations;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IJwtService _jwtService;
    private readonly IAccountService _accountService;

    public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IJwtService jwtService, IAccountService accountService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtService = jwtService;
        _accountService = accountService;
    }

    public async Task<Result<SignUpOutputDto>> SignUp(SignUpInputDto input, CancellationToken cancellationToken)
    {
        var existingUser = await _userManager.FindByNameAsync(input.UserName);

        if(existingUser is not null)
        {
            var message = "The user name is already taken.";
            return new Result<SignUpOutputDto>(new LogicException(message));
        }

        var user = input.Adapt<User>();

        var result = await _userManager.CreateAsync(user, input.Password);

        if (result.Succeeded is false)
        {
            var message = string.Join(" - ", result.Errors.Select(e => e.Description).ToList());
            return new Result<SignUpOutputDto>(new LogicException(message));
        }

        await _accountService.InitializeAccount(new() { UserId = user.Id }, cancellationToken);

        var output = user.Adapt<SignUpOutputDto>();

        return output;
    }
 
    public async Task<Result<SignInOutputDto>> SignIn(SignInInputDto input, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(input.UserName);

        if (user is null)
        {
            var message = "The user name is already exist.";
            return new Result<SignInOutputDto>(new LogicException(message));
        }

        var checkPasswordResult = await _signInManager.CheckPasswordSignInAsync(user, input.Password, lockoutOnFailure: false);


        if (checkPasswordResult.Succeeded is false)
        {
            var message = "The user name or password is wrong.";
            return new Result<SignInOutputDto>(new LogicException(message));
        }

        return await _jwtService.GenerateToken(input);
    }
}
