namespace LanguageExt.Common;

public static class LanguageExtExtensions
{
    public static TResult ToResult<TResult>(this Result<TResult> result) => result.Match((x) => x, null);
}
