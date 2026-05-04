namespace BingoAPI.Models.DTOs.Get;

public sealed record DrawnNumbersResponseDto(string Room, List<int> Numbers);
