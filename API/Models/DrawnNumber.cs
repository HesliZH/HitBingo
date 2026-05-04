namespace BingoAPI.Models;

public class DrawnNumber
{
    public int Id { get; set; }
    public int Number { get; set; }
    public DateTime DrawnAt { get; set; } = DateTime.UtcNow;
    public int SalaId { get; set; }
    public Sala? Sala { get; set; }
}
