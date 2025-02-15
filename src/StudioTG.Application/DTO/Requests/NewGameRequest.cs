using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StudioTG.Application.DTO.Requests
{
    public class NewGameRequest
    {
        [JsonPropertyName("width")]
        [DefaultValue(10)]
        public required int Width { get; set; }
        [JsonPropertyName("height")]
        [DefaultValue(10)]
        public required int Height { get; set; }
        [JsonPropertyName("mines_count")]
        [DefaultValue(10)]
        public required int MinesCount { get; set; }
        public override string ToString()
            => $"{nameof(NewGameRequest)} {{ {nameof(Width)} = {Width}, {nameof(Height)} = {Height}, {nameof(MinesCount)} = {MinesCount} }}";
    }
}
