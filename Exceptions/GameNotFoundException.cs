namespace StudioTgTest.Exceptions;

public class GameNotFoundException : GameException
{
    public GameNotFoundException(Guid gameId) : base($"игра с идентификатором {gameId} не была создана или устарела (неактуальна)")
    {
    }
}
