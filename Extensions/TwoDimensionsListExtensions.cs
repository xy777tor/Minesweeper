namespace StudioTgTest.Extensions;

public static class TwoDimensionsListExtensions
{
    public static (int x, int y)[] GetNeighboursIndexes<T>(this List<List<T>> cells, int row, int col)
    {
        var possibleNeighbours = new (int x, int y)[] {
            (row - 1, col),
            (row + 1, col),
            (row, col + 1),
            (row, col - 1),
            (row - 1, col + 1),
            (row - 1, col - 1),
            (row + 1, col + 1),
            (row + 1, col - 1)
        };

        return possibleNeighbours
            .Where(n => cells.IsValidNeighbour(n.x, n.y))
            .ToArray();
    }

    private static bool IsValidNeighbour<T>(this List<List<T>> cells, int row, int col)
    {
        return row >= 0 &&
               row < cells.Count &&
               col >= 0 &&
               col < cells[row].Count;
    }
}
