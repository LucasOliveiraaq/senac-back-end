using Authentication.Domain.Entities;
using Authentication.Domain.Repositories;
using Authentication.Infrastructure.Respositorie;
using Microsoft.Extensions.Options;

namespace Authentication.Infra
{
    public class UserRepository : IUserRepository
    {
        private string ConnectionString { get; set; }
        public AppDbContext AppDbContext { get; }
        public UserRepository(IOptions<Settings> settings, AppDbContext _AppDbContext)
        {
            ConnectionString = settings.Value.ConnectionString;
            AppDbContext = _AppDbContext;
        }

        public async Task<User> Get(string email, string password)
        {
            return AppDbContext.Users.FirstOrDefault(x => x.Email.ToLower().Equals(email.ToLower(), StringComparison.Ordinal) && x.Password.Equals(password));
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return AppDbContext.Users.ToList();
        }

        public async Task<bool> Check(string email)
        {
            return AppDbContext.Users.Where(x => x.Email.ToLower() == email.ToLower()).ToList() == null;
        }

        public async Task<string> Create(User user)
        {
            try
            {
                await AppDbContext.Users.AddAsync(user);
                await AppDbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return string.Empty;
        }

        public async Task<string> Update(User user)
        {
            try
            {
                AppDbContext.Users.Update(user);
                AppDbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return "Usuario não encontrado";
        }

        public async Task<string> CheckIfIdExist(Guid id)
        {
            var user = AppDbContext.Users.FirstOrDefault(x => x.Id.Equals(id));
            if (user != null)
            {
                return "Usuario não encontrado";
            }
            return string.Empty;
        }

        public async Task<string> Delete(Guid id)
        {
            try
            {
                var user = AppDbContext.Users.FirstOrDefault(x => x.Id.Equals(id));
                if (user != null)
                {
                    return string.Empty;
                }
                AppDbContext.Users.Remove(user);
                await AppDbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return "Usuario não encontrado";
        }

        public UserRepository(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
            if (AppDbContext.Database.EnsureCreated())
            {
                if (AppDbContext.Users != null && AppDbContext.Users.Count<User>() == 0)
                {
                    AppDbContext.Users.Add(new User
                    {
                        Id = Guid.NewGuid(),
                        UserName = "batman",
                        Password = "batman123456",
                        Role = "simples",
                        Email =
                   "batman@test.com"
                    });
                    AppDbContext.Users.Add(new User
                    {
                        Id = Guid.NewGuid(),
                        UserName = "robin",
                        Password = "robin123456",
                        Role = "simples",
                        Email =
                   "robin@test.com"
                    });
                    AppDbContext.Users.Add(new User
                    {
                        Id = Guid.NewGuid(),
                        UserName = "admin",
                        Password = "admin123456",
                        Role = "manager",
                        Email =
                   "admin@test.com"
                    });
                    AppDbContext.SaveChanges();
                }
            }
        }
    }
}
