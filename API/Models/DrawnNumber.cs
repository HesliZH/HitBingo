namespace BingoAPI.Models;

public class DrawnNumber
{
    public int Id { get; set; }
    public int Number { get; set; }
    public DateTime DrawnAt { get; set; } = DateTime.UtcNow;
    public int GameRoomId { get; set; }
    public GameRoom? GameRoom { get; set; }
}
