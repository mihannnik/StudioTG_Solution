using System.ComponentModel;
using System.Text.Json.Serialization;

namespace StudioTG.Application.DTO.Responses
{
    public class ErrorResponse
    {
        [JsonPropertyName("error")]
        [DefaultValue("Произошла непредвиденная ошибка")]
        public required string Error { get; set; }
    }
}
