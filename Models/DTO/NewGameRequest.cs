﻿using System.Text.Json.Serialization;

namespace StudioTgTest.Models.DTO;

public class NewGameRequest
{
    [JsonPropertyName("height")]
    public int Height { get; set; }

    [JsonPropertyName("width")]
    public int Width { get; set; }

    [JsonPropertyName("mines_count")]
    public int MinesCount { get; set; }
}
