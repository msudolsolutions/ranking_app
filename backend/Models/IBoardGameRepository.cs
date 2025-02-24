namespace RankingApp.Models;


public interface IBoardGameRepository
{
    IEnumerable<BoardGameModel> GetAll();
    BoardGameModel? GetById(int id);
    void Add(BoardGameModel game);
    void Update(BoardGameModel game);
    void Delete(int id);
}
