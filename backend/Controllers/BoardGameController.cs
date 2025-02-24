using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using RankingApp.Models;



namespace RankingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoardGameController : ControllerBase
    {
        private static readonly string filePath = "wwwroot/data/games.json";
        private static readonly IEnumerable<BoardGameModel> BoardGames = GetGamesFromJson(filePath);

        [HttpGet]
        public ActionResult<IEnumerable<BoardGameModel>> GetAllGames()
        {
            return Ok(BoardGames);
        }

        internal static IEnumerable<BoardGameModel> GetGamesFromJson(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                return Enumerable.Empty<BoardGameModel>(); ;
            }

            try
            {
                string jsonContent = System.IO.File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<List<BoardGameModel>>(jsonContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading JSON: {ex.Message}");
                return Enumerable.Empty<BoardGameModel>(); ;
            }
        }
    }
}
