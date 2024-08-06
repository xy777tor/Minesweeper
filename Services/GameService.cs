using StudioTgTest.Exceptions;
using StudioTgTest.Models;
using StudioTgTest.Models.Interfaces;

namespace StudioTgTest.Services;

public class GameService(IGameRepository repository) : IGameService
{
    private readonly IGameRepository _gameRepository = repository;

    public Game CreateNewGame(int height, int width, int minesCount)
    {
        Game game = new(width, height, minesCount);

        _gameRepository.CreateNewGame(game);

        return game;
    }

    public Game PerformGameTurn(Guid gameId, int row, int col)
    {
        Game game = _gameRepository.RetrieveGame(gameId);

        if (game.IsCompleted)
            throw new GameException($"Игра с идентификатором {gameId} была завершена");

        ValidateGameTurnParameters(game, row, col);

        if (game.IsStarted == false)
        {
            game.GenerateField(row, col);
            return game;
        }

        game.Turn(row, col);

        return game;
    }

    private static void ValidateGameTurnParameters(Game game, int row, int col)
    {
        if (row < 0 || col < 0)
            throw new GameException("Координаты должны быть неотрицательными");

        if (col >= game.Width)
            throw new GameException($"Координата col должна быть менее ширины поля {game.Width}");

        if (row >= game.Height)
            throw new GameException($"Координата row должна быть менее высоты поля {game.Height}");
    }
}
