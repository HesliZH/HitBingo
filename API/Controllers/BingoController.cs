using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BingoAPI.DAL;
using BingoAPI.Models;
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

    [HttpPost("cartela")]
    public async Task<IActionResult> CreateCartela([FromQuery] Guid roomUuid, [FromQuery] Guid playerUuid, [FromQuery] string? roomName = null)
    {
        if (roomUuid == Guid.Empty || playerUuid == Guid.Empty)
        {
            return BadRequest(new { message = "Os parâmetros 'roomUuid' e 'playerUuid' são obrigatórios." });
        }

        var sala = await _context.Salas
            .Include(s => s.SalasCartelas)
            .FirstOrDefaultAsync(s => s.Uuid == roomUuid);

        if (sala == null)
        {
            sala = new Sala
            {
                Uuid = roomUuid,
                Name = string.IsNullOrWhiteSpace(roomName) ? $"Sala-{roomUuid:N}" : roomName.Trim()
            };
            _context.Salas.Add(sala);
        }

        var card = new BingoCardDto(
            B: GenerateNumbers(1, 15, 5),
            I: GenerateNumbers(16, 30, 5),
            N: GenerateNColumn(),
            G: GenerateNumbers(46, 60, 5),
            O: GenerateNumbers(61, 75, 5)
        );

        var cardJson = JsonSerializer.Serialize(card);
        var salaCartela = new SalaCartela
        {
            Sala = sala,
            SalaUuid = sala.Uuid,
            PlayerUuid = playerUuid,
            CardJson = cardJson,
            CreatedAt = DateTime.UtcNow
        };

        _context.SalasCartelas.Add(salaCartela);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            salaUuid = sala.Uuid,
            playerUuid,
            cartelaId = salaCartela.Id,
            card
        });
    }

    [HttpPost("sorteia")]
    public async Task<IActionResult> Sorteia([FromQuery] string room)
    {
        if (string.IsNullOrWhiteSpace(room))
        {
            return BadRequest(new { message = "O parâmetro 'room' é obrigatório." });
        }

        var roomName = room.Trim();

        var sala = await _context.Salas
            .Include(r => r.DrawnNumbers)
            .FirstOrDefaultAsync(r => r.Name == roomName);

        if (sala == null)
        {
            sala = new Sala { Name = roomName };
            _context.Salas.Add(sala);
        }

        var drawnNumbers = sala.DrawnNumbers.Select(d => d.Number).ToHashSet();
        if (drawnNumbers.Count >= 75)
        {
            return BadRequest(new { message = "Todos os números já foram sorteados nesta sala." });
        }

        var availableNumbers = Enumerable.Range(1, 75)
            .Where(n => !drawnNumbers.Contains(n))
            .ToList();

        var nextNumber = availableNumbers[Random.Shared.Next(availableNumbers.Count)];
        var drawn = new DrawnNumber
        {
            Number = nextNumber,
            DrawnAt = DateTime.UtcNow,
            Sala = sala
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

        var sala = await _context.Salas
            .Include(r => r.DrawnNumbers)
            .FirstOrDefaultAsync(r => r.Name == roomName);

        if (sala == null)
        {
            return NotFound(new { message = "Sala não encontrada." });
        }

        var numbers = sala.DrawnNumbers
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
