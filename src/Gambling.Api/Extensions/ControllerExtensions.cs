using Gambling.Shared.Exceptions;

namespace Microsoft.AspNetCore.Mvc;

public static class ControllerExtensions
{
    public static IActionResult ToOk<TResult, TContract>(this Result<TResult> result, Func<TResult, TContract> mapper)
    {
        return result.Match<IActionResult>(obj =>
        {
            return new OkObjectResult(obj);
        }, exception =>
        {
            if (exception is LogicException logicException)
            {
                return new BadRequestObjectResult(logicException);
            }

            return new StatusCodeResult(500);
        });
    }
}
