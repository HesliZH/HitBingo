using BingoAPI.Models.Enums;

namespace BingoAPI.Models.DTOs.PostPut;

public sealed record CreateSalaDto(
    string Name,
    int MaxPlayers,
    string Password,
    SalaStatus Status
);
