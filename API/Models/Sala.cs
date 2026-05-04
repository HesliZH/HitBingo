namespace BingoAPI.Models;

public class Sala
{
    public int Id { get; set; }
    public Guid Uuid { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public List<SalaCartela> SalasCartelas { get; set; } = new();
    public List<DrawnNumber> DrawnNumbers { get; set; } = new();
}
