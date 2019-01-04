using MediatR;
using MukSoft.Core.Extensions;
using MukSoft.Data.Interfaces;
using MukSoft.Services.Contact.Command;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MukSoft.Services.Contact.CommandHandlers
{
    public class DeleteCommandHandler : IRequestHandler<DeleteCommand, bool>
    {
        private readonly IContactService _service;
        public DeleteCommandHandler(IContactService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            request.ThrowIfNull(nameof(request));
            var contact = await _service.GetContactByIdAsync((Guid)request.Id);
            if (contact == null) { return false; }
            else
            {
                return await _service.DeleteContactAsync(contact);
            }
        }
    }
}
