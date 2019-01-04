using MediatR;
using System;

namespace MukSoft.Services.Contact.Command
{
    public class UpdateCommand : IRequest<bool>
    {
        public Guid id { get; set; }
        public MukSoft.Core.Domain.Contact contactModel { get; set; }
        public UpdateCommand(Guid contactId, MukSoft.Core.Domain.Contact model)
        {
            id = contactId;
            contactModel = model;
        }
    }
}
