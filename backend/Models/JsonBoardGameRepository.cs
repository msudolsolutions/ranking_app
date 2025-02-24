using System.Text.Json;
namespace RankingApp.Models;


public class BoardGameJsonRepository : IBoardGameRepository
{
    private static readonly string filePath = "wwwroot/data/games.json";
    private List<BoardGameModel> BoardGames { get; set; }

    public BoardGameJsonRepository()
    {
        BoardGames = LoadGamesFromJson().ToList();
    }

    public IEnumerable<BoardGameModel> GetAll() => BoardGames;

    public BoardGameModel? GetById(int id) => BoardGames.FirstOrDefault(g => g.Id == id);

    public void Add(BoardGameModel game)
    {
        game.Id = BoardGames.Any() ? BoardGames.Max(g => g.Id) + 1 : 1;
        BoardGames.Add(game);
        SaveGamesToJson();
    }

    public void Update(BoardGameModel game)
    {
        var index = BoardGames.FindIndex(g => g.Id == game.Id);
        if (index != -1)
        {
            BoardGames[index] = game;
            SaveGamesToJson();
        }
    }

    public void Delete(int id)
    {
        var game = BoardGames.FirstOrDefault(g => g.Id == id);
        if (game != null)
        {
            BoardGames.Remove(game);
            SaveGamesToJson();
        }
    }

    private IEnumerable<BoardGameModel> LoadGamesFromJson()
    {
        if (!System.IO.File.Exists(filePath)) return Enumerable.Empty<BoardGameModel>();

        try
        {
            string json = System.IO.File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<BoardGameModel>>(json) ?? new List<BoardGameModel>();
        }
        catch
        {
            return Enumerable.Empty<BoardGameModel>();
        }
    }

    private void SaveGamesToJson()
    {
        try
        {
            string json = JsonSerializer.Serialize(BoardGames, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(filePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving JSON: {ex.Message}");
        }
    }
}
