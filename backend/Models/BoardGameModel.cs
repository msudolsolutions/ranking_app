namespace RankingApp.Models;


public class BoardGameModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int ImageId { get; set; }
    public int Year { get; set; }
    public int MinPlayers { get; set; }
    public int MaxPlayers { get; set; }
    public int MinAge { get; set; }
    public int Duration { get; set; }
    public int Difficulty { get; set; }
    public int Rating { get; set; }
    public int ItemType { get; set; }
}
