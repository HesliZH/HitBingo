using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BingoAPI.DAL;
using BingoAPI.Models.DTOs.Get;

namespace BingoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BingoController : ControllerBase
{
    private readonly BingoDbContext _context;

    public BingoController(BingoDbContext context)
    {
        _context = context;
    }

    [HttpGet("cartela")]
    public ActionResult<BingoCardDto> GetCartela()
    {
        var card = new BingoCardDto(
            B: GenerateNumbers(1, 15, 5),
            I: GenerateNumbers(16, 30, 5),
            N: GenerateNColumn(),
            G: GenerateNumbers(46, 60, 5),
            O: GenerateNumbers(61, 75, 5)
        );

        return Ok(card);
    }

    [HttpPost("sorteia")]
    public async Task<IActionResult> Sorteia([FromQuery] string room)
    {
        if (string.IsNullOrWhiteSpace(room))
        {
            return BadRequest(new { message = "O parâmetro 'room' é obrigatório." });
        }

        var roomName = room.Trim();

        var gameRoom = await _context.GameRooms
            .Include(r => r.DrawnNumbers)
            .FirstOrDefaultAsync(r => r.Name == roomName);

        if (gameRoom == null)
        {
            gameRoom = new Models.GameRoom { Name = roomName };
            _context.GameRooms.Add(gameRoom);
        }

        var drawnNumbers = gameRoom.DrawnNumbers.Select(d => d.Number).ToHashSet();
        if (drawnNumbers.Count >= 75)
        {
            return BadRequest(new { message = "Todos os números já foram sorteados nesta sala." });
        }

        var availableNumbers = Enumerable.Range(1, 75)
            .Where(n => !drawnNumbers.Contains(n))
            .ToList();

        var nextNumber = availableNumbers[Random.Shared.Next(availableNumbers.Count)];
        var drawn = new Models.DrawnNumber
        {
            Number = nextNumber,
            DrawnAt = DateTime.UtcNow,
            GameRoom = gameRoom
        };

        _context.DrawnNumbers.Add(drawn);
        await _context.SaveChangesAsync();

        return Ok(new DrawNumberResponseDto(nextNumber, drawn.DrawnAt, roomName));
    }

    [HttpGet("sorteados")]
    public async Task<IActionResult> GetSorteados([FromQuery] string room)
    {
        if (string.IsNullOrWhiteSpace(room))
        {
            return BadRequest(new { message = "O parâmetro 'room' é obrigatório." });
        }

        var roomName = room.Trim();

        var gameRoom = await _context.GameRooms
            .Include(r => r.DrawnNumbers)
            .FirstOrDefaultAsync(r => r.Name == roomName);

        if (gameRoom == null)
        {
            return NotFound(new { message = "Sala não encontrada." });
        }

        var numbers = gameRoom.DrawnNumbers
            .OrderBy(d => d.DrawnAt)
            .Select(d => d.Number)
            .ToList();

        return Ok(new DrawnNumbersResponseDto(roomName, numbers));
    }

    private static List<int> GenerateNumbers(int minInclusive, int maxInclusive, int count)
    {
        return Enumerable.Range(minInclusive, maxInclusive - minInclusive + 1)
            .OrderBy(_ => Random.Shared.Next())
            .Take(count)
            .ToList();
    }

    private static List<int?> GenerateNColumn()
    {
        var numbers = GenerateNumbers(31, 45, 4)
            .Select(n => (int?)n)
            .ToList();

        numbers.Insert(2, null);
        return numbers;
    }
}

public sealed record BingoCardDto(
    List<int> B,
    List<int> I,
    List<int?> N,
    List<int> G,
    List<int> O
);
