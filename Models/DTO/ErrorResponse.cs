using System.Text.Json.Serialization;

namespace StudioTgTest.Models.DTO;

internal class ErrorResponse
{
    [JsonPropertyName("error")]
    public string ErrorMessage { get; set; }

    public ErrorResponse(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }
}