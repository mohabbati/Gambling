using Gambling.Models.Account;
using Gambling.Models.Game;
using Gambling.Models.Identity;
using Gambling.Shared.Dtos.Game;
using Gambling.Shared.Dtos.Account;

namespace Gambling.Test.Services;

public class GameServiceTests
{
    private readonly GameService _sut;
    private readonly Mock<IAccountService> _moqAccountService = new Mock<IAccountService>();
    private readonly Mock<IRandomService> _moqRandomService = new Mock<IRandomService>();

    private readonly Guid moqUserId = Guid.NewGuid();
    private readonly Guid moqAccountId = Guid.NewGuid();

    public GameServiceTests()
    {
        var moqDbContext = MockGamblingDbContext();
        var moqUserManager = MockUserManager();

        _sut = new GameService(moqDbContext.Object, moqUserManager.Object, _moqAccountService.Object, _moqRandomService.Object);
    }

    private Mock<GamblingDbContext> MockGamblingDbContext()
    {
        var dbContextoptions = new DbContextOptionsBuilder<GamblingDbContext>().UseSqlServer().Options;
        var moqDbContext = new Mock<GamblingDbContext>(dbContextoptions);

        var accounts = new List<Account>
            {
                new Account
                {
                    Id = moqAccountId,
                    UserId = moqUserId,
                    Balance = 10000
                },
            };

        var plays = new List<Play>();

        moqDbContext.Setup(x => x.Accounts).ReturnsDbSet(accounts);
        moqDbContext.Setup(x => x.Plays).ReturnsDbSet(plays);

        return moqDbContext;
    }

    private Mock<UserManager<User>> MockUserManager()
    {
        var store = new Mock<IUserStore<User>>();
        var moqUserManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

        moqUserManager.Object.UserValidators.Add(new UserValidator<User>());
        moqUserManager.Object.PasswordValidators.Add(new PasswordValidator<User>());

        var user = new User()
        {
            Id = moqUserId,
            UserName = "test",
            FullName = "Test"
        };

        moqUserManager.Setup(x => x.FindByIdAsync(moqUserId.ToString()))
            .ReturnsAsync(user);

        return moqUserManager;
    }

    [Fact]
    public async Task ChanceNumberBetween0to9MustWork()
    {
        // Arrange
        _moqAccountService.Setup(x => x.WithdrawAsync(It.IsAny<WithdrawInputDto>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(It.IsAny<WithdrawOutputDto>());

        _moqAccountService.Setup(x => x.DepositAsync(It.IsAny<DepositInputDto>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(It.IsAny<DepositOutputDto>());

        var input = new PlayInputDto
        {
            UserId = moqUserId,
            BetAmount = 100,
            ChanceNumber = 0
        };

        //Act
        var result = (await _sut.PlayAsync(input, CancellationToken.None)).ToResult();

        //Assert
        Assert.IsType<PlayOutputDto>(result);
    }

    [Fact]
    public async Task WhenWonMustAdd9TimesBetAmountToPoints()
    {
        // Arrange
        int balance = 10000;
        int betAmount = 100;
        byte chanceNumber = 7;
        
        _moqRandomService.Setup(x => x.Generate(0, 9 + 1)).Returns(chanceNumber);

        var withrawOutput = new Result<WithdrawOutputDto>(new WithdrawOutputDto()
        {
            Balance = balance - betAmount
        });
        _moqAccountService.Setup(x => x.WithdrawAsync(It.IsAny<WithdrawInputDto>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(withrawOutput);

        var depositOutput = new Result<DepositOutputDto>(new DepositOutputDto()
        {
            Balance = balance - betAmount + (betAmount * 9)
        });
        _moqAccountService.Setup(x => x.DepositAsync(It.IsAny<DepositInputDto>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(depositOutput);

        var input = new PlayInputDto
        {
            UserId = moqUserId,
            BetAmount = betAmount,
            ChanceNumber = chanceNumber
        };

        //Act
        var result = (await _sut.PlayAsync(input, CancellationToken.None)).ToResult();
        
        //Assert
        Assert.True(result.Points == (betAmount * 9).ToString().ToPlusPoint());
    }
}