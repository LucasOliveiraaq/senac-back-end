using Authentication.Domain.Entities;

namespace Authentication.Domain.Repositories
{
    public interface IGameRepository
    {
        Task<Game> Get(string nome);
        Task<bool> Check(string nome);
        Task<string> CheckIfIdExist(Guid id);
        Task<string> Create(Game game);
        Task<string> Delete(Guid id);
        Task<IEnumerable<Game>> GetAll();
        Task<string> Update(Game game);
    }
}
