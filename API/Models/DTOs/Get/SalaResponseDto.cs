using BingoAPI.Models.Enums;

namespace BingoAPI.Models.DTOs.Get;

public sealed record SalaResponseDto(
    Guid Uuid,
    string Name,
    int MaxPlayers,
    SalaStatus Status,
    DateTime CreatedAt
);
