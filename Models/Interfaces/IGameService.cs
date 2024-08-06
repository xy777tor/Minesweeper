namespace StudioTgTest.Models.Interfaces;

public interface IGameService
{
    Game CreateNewGame(int height, int width, int minesCount);

    Game PerformGameTurn(Guid gameId, int row, int col);
}