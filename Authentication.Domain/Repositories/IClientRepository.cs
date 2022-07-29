using Authentication.Domain.Entities;

namespace Authentication.Domain.Repositories
{
    public interface IClientRepository
    {
        Task<Client> Get(string nome, int idade);
        Task<bool> Check(string nome);
        Task<string> CheckIfIdExist(Guid id);
        Task<string> Create(Client client);
        Task<string> Delete(Guid id);
        Task<IEnumerable<Client>> GetAll();
        Task<string> Update(Client client);
    }
}
