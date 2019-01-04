using MediatR;
using MukSoft.Core.DTO;
using System;
namespace MukSoft.Services.Contact.Query
{
    public class ReadByIdQuery : IRequest<ContactDto>
    {
        public Guid Id { get; set; }
    }

}
