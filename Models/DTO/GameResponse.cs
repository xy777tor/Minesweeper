using System.Text.Json.Serialization;

namespace StudioTgTest.Models.DTO;

public class GameResponse
{
    [JsonPropertyName("game_id")]
    public Guid GameId { get; set; }

    [JsonPropertyName("width")]
    public int Width { get; set; }

    [JsonPropertyName("height")]
    public int Height { get; set; }

    [JsonPropertyName("mines_count")]
    public int MinesCount { get; set; }

    [JsonPropertyName("completed")]
    public bool IsCompleted { get; set; }

    [JsonPropertyName("field")]
    public List<List<string>> Field { get; set; } = null!;
}
