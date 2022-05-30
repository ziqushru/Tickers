using System.Text.Json;

namespace Entities.Dto;

public record ErrorResponseDto
{
    public int status_code { get; init; }
    public string message { get; init; }
    
    public override string ToString() => JsonSerializer.Serialize(this);
}
