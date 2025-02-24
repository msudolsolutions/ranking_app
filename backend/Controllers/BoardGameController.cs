using Microsoft.AspNetCore.Mvc;
using RankingApp.Models;
namespace RankingApp.Controllers;


[ApiController]
[Route("api/[controller]")]
public class BoardGameController : ControllerBase
{
    private readonly IBoardGameRepository _repository;

    public BoardGameController(IBoardGameRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<BoardGameModel>> ReadAll()
    {
        return Ok(_repository.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<BoardGameModel> Read(int id)
    {
        var game = _repository.GetById(id);
        if (game == null) return NotFound();
        return Ok(game);
    }

    [HttpPost]
    public IActionResult Create([FromBody] BoardGameModel newBoardGame)
    {
        if (newBoardGame == null) return BadRequest("Invalid game data");

        _repository.Add(newBoardGame);
        return CreatedAtAction(nameof(Read), new { id = newBoardGame.Id }, newBoardGame);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] BoardGameModel updatedGame)
    {
        if (updatedGame == null || id != updatedGame.Id)
            return BadRequest("Invalid game data");

        var existingGame = _repository.GetById(id);
        if (existingGame == null) return NotFound("Game not found");

        _repository.Update(updatedGame);
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var game = _repository.GetById(id);
        if (game == null) return NotFound("Game not found");

        _repository.Delete(id);
        return Ok();
    }
}
