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
        private static readonly List<BoardGameModel> BoardGames = GetGamesFromJson().ToList();

        [HttpPost]
        public IActionResult Create([FromBody] BoardGameModel newBoardGame)
        {
            if (newBoardGame == null)
            {
                return BadRequest("Invalid game data");
            }

            newBoardGame.Id = BoardGames.Count > 0 ? BoardGames.Max(g => g.Id) + 1 : 1;
            BoardGames.Add(newBoardGame);
            SaveGamesToJson(BoardGames);
            return Ok();
        }

        [HttpGet("{boardGameId}")]
        public ActionResult<BoardGameModel> Read(int boardGameId)
        {
            var boardGame = BoardGames.FirstOrDefault(g => g.Id == boardGameId);
            if (boardGame == null)
            {
                return NotFound();
            }
            return Ok(boardGame);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] BoardGameModel updatedGame)
        {
            if (updatedGame == null || id != updatedGame.Id)
                return BadRequest("Invalid game data");

            var index = BoardGames.FindIndex(g => g.Id == id);
            if (index == -1)
                return NotFound("Game not found");

            BoardGames[index] = updatedGame;
            SaveGamesToJson(BoardGames);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var gameToRemove = BoardGames.FirstOrDefault(g => g.Id == id);
            if (gameToRemove == null)
                return NotFound("Game not found");

            BoardGames.Remove(gameToRemove);
            SaveGamesToJson(BoardGames);
            return Ok();
        }

        [HttpGet]
        public ActionResult<IEnumerable<BoardGameModel>> ReadAll()
        {
            return Ok(BoardGames);
        }

        private static IEnumerable<BoardGameModel> GetGamesFromJson()
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

        private void SaveGamesToJson(List<BoardGameModel> games)
        {
            try
            {
                string jsonContent = JsonSerializer.Serialize(games, new JsonSerializerOptions { WriteIndented = true });
                System.IO.File.WriteAllText(filePath, jsonContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving JSON: {ex.Message}");
            }
        }
    }
}
