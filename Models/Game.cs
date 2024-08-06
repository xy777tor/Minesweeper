using StudioTgTest.Exceptions;
using StudioTgTest.Extensions;

namespace StudioTgTest.Models;

public sealed class Game
{
    public Guid GameId { get; private set; } = Guid.NewGuid();

    public int Height { get; private set; }

    public int Width { get; private set; }

    public int MinesCount { get; private set; }

    public bool IsCompleted { get; private set; } = false;

    public bool IsStarted { get; private set; } = false;

    public List<List<Cell>> Field { get; private set; } = [];

    private (int row, int col)[] _minesCoordinates = [];
    private int _revealedCellsCount = 0;

    public Game(int width, int height, int minesCount)
    {
        Height = height;
        Width = width;
        MinesCount = minesCount;

        for (int i = 0; i < height; i++)
        {
            List<Cell> row = [];

            for (int j = 0; j < width; j++)
            {
                row.Add(new Cell());
            }

            Field.Add(row);
        }
    }

    public void GenerateField(int row, int col)
    {
        FieldInit(row, col);

        IsStarted = true;
    }

    public void Turn(int row, int col)
    {
        if (Field[row][col].Value == Cell.MineToBlow)
        {
            RevealAllCells();
            IsCompleted = true;
            return;
        }

        Reveal(row, col);
    }

    public List<List<string>> ConvertFieldToString2DimensionsList()
    {
        List<List<string>> result = [];

        for (int i = 0; i < Field.Count; i++)
        {
            result.Add([]);
            for (int j = 0; j < Field[i].Count; j++)
            {
                result.Last().Add(Field[i][j].DisplayedValue.ToString());
            }
        }

        return result;
    }

    public override int GetHashCode() => GameId.GetHashCode();

    public override bool Equals(object? obj) => obj is Game game && GameId.Equals(game.GameId);

    private void RevealAllCells()
    {
        for (int i = 0; i < Field.Count; i++)
        {
            for (int j = 0; j < Field[i].Count; j++)
            {
                Field[i][j].IsRevealed = true;
            }
        }
    }

    private void FillCellsWithMines()
    {
        for (int i = 0; i < MinesCount; i++)
        {
            Field[_minesCoordinates[i].row][_minesCoordinates[i].col].Value = Cell.MineToBlow;
        }
    }

    private void FieldInit(int row, int col)
    {
        _minesCoordinates = GenerateRandomMinesCoordinates(row, col);

        FillCellsWithMines();
        FillCellsWithNumbers();
        Reveal(row, col);
    }

    private void Reveal(int row, int col)
    {
        Cell cell = Field[row][col];

        if (cell.IsRevealed)
            throw new GameException("€чейка уже открыта");

        if (cell.Value != Cell.Zero)
        {
            cell.IsRevealed = true;
            _revealedCellsCount++;
            CheckWictoryCondition();
            return;
        }

        Queue<(int, int)> queue = new();
        queue.Enqueue((row, col));

        while (queue.Count > 0)
        {
            (int currX, int currY) = queue.Dequeue();

            if (!IsInBounds(currX, currY) || Field[currX][currY].IsRevealed)
            {
                continue;
            }

            Field[currX][currY].IsRevealed = true;
            _revealedCellsCount++;

            if (Field[currX][currY].Value == Cell.Zero)
            {
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if (IsInBounds(currX + i, currY + j) && !(i == 0 && j == 0))
                        {
                            queue.Enqueue((currX + i, currY + j));
                        }
                    }
                }
            }
        }

        CheckWictoryCondition();
    }

    private void CheckWictoryCondition()
    {
        if (_revealedCellsCount == Height * Width - MinesCount)
        {
            for (int i = 0; i < MinesCount; i++)
            {
                Field[_minesCoordinates[i].row][_minesCoordinates[i].col].Value = Cell.VictoryMine;
                Field[_minesCoordinates[i].row][_minesCoordinates[i].col].IsRevealed = true;
            }

            IsCompleted = true;
        }
    }

    private bool IsInBounds(int x, int y)
    {
        return x >= 0 && x < Height && y >= 0 && y < Width;
    }

    private void FillCellsWithNumbers()
    {
        for (int i = 0; i < MinesCount; i++)
        {
            (int row, int col)[] neighboursIndexes =
                Field.GetNeighboursIndexes(_minesCoordinates[i].row, _minesCoordinates[i].col);

            for (int j = 0; j < neighboursIndexes.Length; j++)
            {
                Cell cell = Field[neighboursIndexes[j].row][neighboursIndexes[j].col];

                if (cell.IsMine)
                    continue;

                if (int.TryParse(cell.Value.ToString(), out int cellValue))
                {
                    cell.Value = (++cellValue).ToChar();
                }
            }
        }
    }

    private (int row, int col)[] GenerateRandomMinesCoordinates(int row, int col)
    {
        (int row, int col)[] minesCoordinates = new (int row, int col)[MinesCount];

        Random random = new();
        HashSet<string> coordinatesSet = [];

        int newRow = random.Next(Height);
        int newCol = random.Next(Width);

        for (int i = 0; i < MinesCount; i++)
        {
            while (coordinatesSet.Contains($"{newCol}{newRow}") || (newCol == col && newRow == row))
            {
                newRow = random.Next(Height);
                newCol = random.Next(Width);
            }

            coordinatesSet.Add($"{newCol}{newRow}");
            minesCoordinates[i].row = newRow;
            minesCoordinates[i].col = newCol;
        }

        return minesCoordinates;
    }
}
