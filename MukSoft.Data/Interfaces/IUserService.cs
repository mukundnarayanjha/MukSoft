using MukSoft.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MukSoft.Data.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUserAsync();
        //Task<User> GetUserByIdAsync(Guid userId);
        Task<User> GetUserByIdAsync(string Key1, string Key2);
        Task<string> CreateUserAsync(User model);
        //Task<bool> UpdateUserAsync(User model);
        //Task<bool> DeleteUserAsync(Guid Id);
    }
}
