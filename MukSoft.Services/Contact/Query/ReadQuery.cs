using MediatR;
using MukSoft.Core.DTO;
using System.Collections.Generic;

namespace MukSoft.Services.Contact.Query
{
    public class ReadQuery : IRequest<IEnumerable<ContactDto>>
    {

    }
}
