using MediatR;
using System;

namespace MukSoft.Services.Contact.Command
{
    public class InsertCommand : IRequest<Guid>
    {
        public MukSoft.Core.Domain.Contact _contact;
        public InsertCommand(MukSoft.Core.Domain.Contact contact)
        {
            _contact = contact;
        }
    }
}
