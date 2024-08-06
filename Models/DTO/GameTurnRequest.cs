using System.Text.Json.Serialization;

namespace StudioTgTest.Models.DTO;

public class GameTurnRequest
{
    [JsonPropertyName("game_id")]
    public Guid GameId { get; set; }

    [JsonPropertyName("col")]
    public int Col {  get; set; }

    [JsonPropertyName("row")]
    public int Row { get; set; }
}
