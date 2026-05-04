namespace BingoAPI.Models.DTOs.Get;

public sealed record DrawNumberResponseDto(int Number, DateTime DrawnAt, string Room);
