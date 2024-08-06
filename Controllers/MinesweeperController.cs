using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using StudioTgTest.Models;
using StudioTgTest.Models.DTO;
using StudioTgTest.Models.Interfaces;
using System.Net.Mime;
using System.Reflection.Metadata.Ecma335;

namespace StudioTgTest.Controllers;

[ApiController]
public class MinesweeperController(IGameService gameService) : ControllerBase
{
    private readonly IGameService _gameService = gameService;

    [HttpPost("new")]
    [ProducesResponseType(typeof(GameResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public IResult CreateNewGame(NewGameRequest request)
    {
        if (request.Height is < 2 or > 30)
            return Results.BadRequest(
                new ErrorResponse("¬ысота пол€ не должна быть менее 2 и более 30"));

        if (request.Width is < 2 or > 30)
            return Results.BadRequest(
                new ErrorResponse("Ўирина пол€ не должна быть менее 2 и более 30"));

        int fieldArea = request.Height * request.Width;

        if (request.MinesCount < 1 || request.MinesCount >= fieldArea)
            return Results.BadRequest(
                new ErrorResponse($" оличество мин не должно быть менее 1 и более {fieldArea - 1}"));

        Game game = _gameService.CreateNewGame(request.Height, request.Width, request.MinesCount);

        return Results.Ok(MapModelToDTO(game));
    }

    [HttpPost("turn")]
    [ProducesResponseType(typeof(GameResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public IResult PerformGameTurn(GameTurnRequest request)
    {
        Game game = _gameService.PerformGameTurn(request.GameId, request.Row, request.Col);

        return Results.Ok(MapModelToDTO(game));
    }

    private static GameResponse MapModelToDTO(Game game)
    {
        return new()
        {
            GameId = game.GameId,
            Field = game.ConvertFieldToString2DimensionsList(),
            Height = game.Height,
            Width = game.Width,
            MinesCount = game.MinesCount,
            IsCompleted = game.IsCompleted,
        };
    }
}
