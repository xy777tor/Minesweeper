namespace StudioTgTest.Extensions;

public static class Int32Extensions
{
    public static char ToChar(this int number)
    {
        bool isNotDigit = number < 0 || number > 9;

        if (isNotDigit) throw
                new ArgumentOutOfRangeException("Invalid range of number because number is not a digit", nameof(number));

        return (char)(number + 48);
    }
}
