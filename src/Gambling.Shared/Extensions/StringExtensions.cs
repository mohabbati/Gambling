namespace System;

public static class StringExtensions
{
    public static string ToPlusPoint(this string value)
    {
        return $"+{value}";
    }
}
