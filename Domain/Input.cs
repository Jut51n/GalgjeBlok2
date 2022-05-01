using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Galgje.Tests")]

namespace Domain;

public static class Input
{
    public static char StringToChar(string input)
    {
        if (input.Count() == 1)
            return input[0];
        else
            throw new ArgumentException("1 letter per beurt opgeven!");
    }

    public static void IsNotSpecial(char input)
    {
        if (!char.IsLetterOrDigit(input))
            throw new ArgumentException("Input moet een letter zijn!");
    }

    public static void IsNotNumeric(char input)
    {
        if (char.IsNumber(input))
            throw new ArgumentException("Input moet een letter zijn");
    }

    public static char ToLower(char input)
    {
        return char.ToLower(input);
    }

}
