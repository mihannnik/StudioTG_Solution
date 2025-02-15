using System.Text.Json.Serialization;

namespace StudioTG.Application.DTO.Responses
{
    public class FieldStateResponse
    {
        [JsonPropertyName("game_id")]
        public required Guid Id { get; set; }

        [JsonPropertyName("width")]
        public required int Width { get; init; }

        [JsonPropertyName("height")]
        public required int Height { get; init; }

        [JsonPropertyName("mines_count")]
        public required int MinesCount { get; init; }

        [JsonPropertyName("completed")]
        public required bool IsCompleted { get; set; }

        [JsonPropertyName("field")]
        public required char[][] Cells { get; set; }
    }
}
