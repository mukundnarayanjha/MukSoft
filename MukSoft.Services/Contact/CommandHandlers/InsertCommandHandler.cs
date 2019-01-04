using MediatR;
using MukSoft.Data.Interfaces;
using MukSoft.Services.Contact.Command;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MukSoft.Services.Contact.CommandHandlers
{
    public class InsertCommandHandler : IRequestHandler<InsertCommand, Guid>
    {
        private readonly IContactService _service;
        public InsertCommandHandler(IContactService service)
        {
            _service = service;
        }
        public async Task<Guid> Handle(InsertCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            Guid id = await _service.CreateContactAsync(request._contact);
            return id;
        }
    }
}
