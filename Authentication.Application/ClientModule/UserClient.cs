using Authentication.Domain.Entities;
using Authentication.Domain.Repositories;

namespace Authentication.Application.ClientModule
{
    public interface IClient
    {
        Task<IEnumerable<ClientModule>> GetAll();
        Task<string> Create(ClientModule client);
        Task<bool> Check(string email);
        Task<string> CheckIfIdExist(Guid id);
        Task<string> Delete(Guid id);
        Task<string> Update(ClientModule client);
        Task<string> ValidateUserData(ClientModule clientModule);
    }


    public class UserClient : IClient
    {
        private IClientRepository _clientRepository { get; }
        private ILogger<ClientModule> _logger { get; }

        public UserClient(IClientRepository clientRepository, ILogger<ClientModule> logger)
        {
            _clientRepository = clientRepository;
            _logger = logger;
        }
        public async Task<string> ValidateUserData(ClientModule clientModule)
        {
            if (clientModule == null)
            {
                return "Dados invalidos";
            }
            if (string.IsNullOrEmpty(clientModule.id.ToString()) == true && await Check(clientModule.Nome))
            {
                return "Usuario já existe";
            }
            return string.Empty;
        }
        public async Task<IEnumerable<ClientModule>> GetAll()
        {
            var clients = await _clientRepository.GetAll();
            var clientModel = new List<ClientModule>();
            foreach (var client in clients)
            {
                clientModel.Add(new ClientModule()
                {
                    Nome = client.Nome,
                    Idade = client.Idade,
                }); 
            }
            return clientModel;
        }
        public async Task<string> Create(ClientModule client)
        {
            string error = await ValidateUserData(client);
            if (String.IsNullOrEmpty(error))
            {
                var clientevo = module(client);
                error = await _clientRepository.Create(clientevo);
                if (String.IsNullOrEmpty(error))
                {
                    return string.Empty;
                }
            }
            return error;
        }


        public Task<bool> Check(string email)
        {
            throw new NotImplementedException();
        }

        public Task<string> CheckIfIdExist(Guid id)
        {
            throw new NotImplementedException();
        }


        public Task<string> Delete(Guid id)
        {
            throw new NotImplementedException();
        }


        public Task<string> Update(ClientModule client)
        {
            throw new NotImplementedException();
        }

        public ClientModule module(ClientModule clientModule)
        {
            var client = new ClientModule()
            {
                Nome = clientModule.Nome,
                Idade =  clientModule.Idade
            };
            client.id = string.IsNullOrEmpty(clientModule.id!.ToString()) ? clientModule.id : client.id;
            return client;
        }
    }
}
