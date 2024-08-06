namespace StudioTgTest.Models.Interfaces;
public interface IGameRepository
{
    void CreateNewGame(Game game);

    Game RetrieveGame(Guid gameId);
}