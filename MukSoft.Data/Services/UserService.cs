using MukSoft.Core.Context;
using MukSoft.Core.Domain;
using MukSoft.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MukSoft.Data.Services
{
    public class UserService : RepositoryBase<User>, IUserService
    {
        public UserService(ApplicationDbContext context) : base(context)
        { }

        public async Task<string> CreateUserAsync(User user)
        {
            Create(user);
            await SaveAsync();
            return user.Id.ToString();
        }

        public async Task<User> GetUserByIdAsync(string key1, string key2)
        {
            var user = await FindByConditionAync(o => o.Email.Equals(key1) && o.Password.Equals(key2));
            return user.FirstOrDefault();
        }

        public async Task<IEnumerable<User>> GetAllUserAsync()
        {
            var users = await FindAllAsync();
            return users.OrderBy(x => x.Name);
        }
    }
}
