using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Galgje.Tests")]

namespace Domain;

public static class Input
{
    public static char Validator(string input)
    {
        char c = StringToChar(input);

        IsNotSpecial(c);
        IsNotNumeric(c);
        return ToLower(c);
    }

    internal static char StringToChar(string input)
    {
        if (input.Count() == 1)
            return input[0];
        else
            throw new ArgumentException("1 letter per beurt opgeven!");
    }

    internal static void IsNotSpecial(char input)
    {
        if (!char.IsLetterOrDigit(input))
            throw new ArgumentException("Input moet een letter zijn!");
    }

    internal static void IsNotNumeric(char input)
    {
        if (char.IsNumber(input))
            throw new ArgumentException("Input moet een letter zijn");
    }

    internal static char ToLower(char input)
    {
        return char.ToLower(input);
    }

}
