namespace StudioTgTest.Models;

public class Cell
{
    public const char Empty = ' ';
    public const char Zero = '0';
    public const char MineToBlow = 'X';
    public const char VictoryMine = 'M';

    public char Value { get; set; } = Zero;

    public bool IsRevealed { get; set; } = false;

    public char DisplayedValue => IsRevealed ? Value : Empty;

    public bool IsMine => Value.Equals(MineToBlow);
}