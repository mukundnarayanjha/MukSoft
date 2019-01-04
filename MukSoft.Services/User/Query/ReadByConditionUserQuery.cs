using MediatR;
using MukSoft.Core.DTO;
namespace MukSoft.Services.User.Query
{
    public class ReadByConditionUserQuery : IRequest<LoginDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public ReadByConditionUserQuery(string email, string password)
        {
            Password = password;
            Email = email;
        }

    }

}
