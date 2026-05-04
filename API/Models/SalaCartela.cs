namespace BingoAPI.Models;

public class SalaCartela
{
    public int Id { get; set; }
    public Guid SalaUuid { get; set; }
    public Guid PlayerUuid { get; set; }
    public string CardJson { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int SalaId { get; set; }
    public Sala? Sala { get; set; }
}
