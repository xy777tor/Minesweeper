using StudioTgTest.Exceptions;
using StudioTgTest.Models;
using StudioTgTest.Models.Interfaces;

namespace StudioTgTest.Persistance;

public class GameRepositoryMock : IGameRepository
{
    public Dictionary<Guid, Game> Games { get; set; } = new Dictionary<Guid, Game>();

    public void CreateNewGame(Game game)
    {
        Games.Add(game.GameId, game);
    }

    public Game RetrieveGame(Guid gameId)
    {
        Games.TryGetValue(gameId, out Game? game);

        if (game is null)
            throw new GameNotFoundException(gameId);

        return game;
    }
}
