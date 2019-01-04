using MediatR;
using System;

namespace MukSoft.Services.Contact.Command
{
    public class DeleteCommand : IRequest<bool>
    {
        public Guid? Id { get; set; }
        public DeleteCommand(Guid? id)
        {
            Id = id;
        }
    }
}
