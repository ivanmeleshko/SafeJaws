using System;
using System.Linq;

public static class GameExtensions
{
    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    public static string Random(this string target, int length)
    {
        Random rnd = new Random();
        string pattern = target != "" ? target : chars;

        return new string(Enumerable.Repeat(pattern, length).Select(s => s[rnd.Next(s.Length)]).ToArray());
    }
}
