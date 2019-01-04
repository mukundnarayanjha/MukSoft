using MediatR;
using MukSoft.Data.Interfaces;
using MukSoft.Services.User.Command;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MukSoft.Services.User.CommandHandlers
{
    public class InsertUserCommandHandler : IRequestHandler<InsertUserCommand, string>
    {
        private readonly IUserService _service;
        public InsertUserCommandHandler(IUserService service)
        {
            _service = service;
        }
        public async Task<string> Handle(InsertUserCommand request, CancellationToken cancellationToken)
        {
            string id = string.Empty;
            if (request == null) throw new ArgumentNullException(nameof(request));
            id = await _service.CreateUserAsync(request._user);
            return id;
        }
    }
}
