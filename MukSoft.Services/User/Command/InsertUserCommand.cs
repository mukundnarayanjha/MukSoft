using MediatR;

namespace MukSoft.Services.User.Command
{
    public class InsertUserCommand : IRequest<string>
    {
        public MukSoft.Core.Domain.User _user;
        public InsertUserCommand(MukSoft.Core.Domain.User user)
        {
            _user = user;
        }
    }
}
