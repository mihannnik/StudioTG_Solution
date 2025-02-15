using System.Text.Json.Serialization;

namespace StudioTG.Application.DTO.Requests
{
    public class MakeTurnRequest
    {
        [JsonPropertyName("game_id")]
        public required Guid Id { get; set; }

        [JsonPropertyName("row")]
        public int Row { get; set; }

        [JsonPropertyName("col")]
        public int Collumn { get; set; }

        public override string ToString()
            => $"{nameof(MakeTurnRequest)} {{ {nameof(Id)} = {Id}, {nameof(Row)} = {Row}, {nameof(Collumn)} = {Collumn} }}";
    }
}
