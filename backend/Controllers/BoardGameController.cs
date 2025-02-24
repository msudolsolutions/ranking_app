using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RankingApp.Models;

namespace RankingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoardGameController : ControllerBase
    {
        private static readonly string filePath = "wwwroot/data/games.json";
        private readonly BoardGameDbContext _context;

        public BoardGameController(BoardGameDbContext context)
        {
            _context = context;

            // Load JSON data to database if empty
            if (!_context.BoardGames.Any())
            {
                var games = GetGamesFromJson().ToList();
                if (games.Any())
                {
                    _context.BoardGames.AddRange(games);
                    _context.SaveChanges();
                }
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] BoardGameModel newBoardGame)
        {
            if (newBoardGame == null)
            {
                return BadRequest("Invalid game data");
            }

            newBoardGame.Id = _context.BoardGames.Any() ? _context.BoardGames.Max(g => g.Id) + 1 : 1;
            _context.BoardGames.Add(newBoardGame);
            _context.SaveChanges();

            var games = _context.BoardGames.ToList();
            SaveGamesToJson(games);
            return CreatedAtAction(nameof(Read), new { boardGameId = newBoardGame.Id }, newBoardGame);
        }

        [HttpGet("{boardGameId}")]
        public ActionResult<BoardGameModel> Read(int boardGameId)
        {
            var boardGame = _context.BoardGames.Find(boardGameId);
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

            var game = _context.BoardGames.Find(id);
            if (game == null)
                return NotFound("Game not found");

            _context.Entry(game).CurrentValues.SetValues(updatedGame);
            _context.SaveChanges();

            var games = _context.BoardGames.ToList();
            SaveGamesToJson(games);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var gameToRemove = _context.BoardGames.Find(id);
            if (gameToRemove == null)
                return NotFound("Game not found");

            _context.BoardGames.Remove(gameToRemove);
            _context.SaveChanges();

            var games = _context.BoardGames.ToList();
            SaveGamesToJson(games);
            return Ok();
        }

        [HttpGet]
        public ActionResult<IEnumerable<BoardGameModel>> ReadAll()
        {
            return Ok(_context.BoardGames.ToList());
        }

        private static IEnumerable<BoardGameModel> GetGamesFromJson()
        {
            if (!System.IO.File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                return Enumerable.Empty<BoardGameModel>();
            }

            try
            {
                string jsonContent = System.IO.File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<List<BoardGameModel>>(jsonContent) ?? new List<BoardGameModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading JSON: {ex.Message}");
                return Enumerable.Empty<BoardGameModel>();
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
