namespace BingoAPI.Models;

public class GameRoom
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public List<DrawnNumber> DrawnNumbers { get; set; } = new();
}
