using Microsoft.EntityFrameworkCore;
namespace RankingApp.Models;


public class SQLiteBoardGameRepository : IBoardGameRepository
{
    private readonly BoardGameDbContext _context;

    public SQLiteBoardGameRepository(BoardGameDbContext context)
    {
        _context = context;
    }

    public IEnumerable<BoardGameModel> GetAll() => _context.BoardGames.ToList();

    public BoardGameModel? GetById(int id) => _context.BoardGames.Find(id);

    public void Add(BoardGameModel game)
    {
        _context.BoardGames.Add(game);
        _context.SaveChanges();
    }

    public void Update(BoardGameModel game)
    {
        _context.Entry(game).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var game = _context.BoardGames.Find(id);
        if (game != null)
        {
            _context.BoardGames.Remove(game);
            _context.SaveChanges();
        }
    }
}
