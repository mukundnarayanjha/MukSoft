using MediatR;
using MukSoft.Core.DTO;
using System.Collections.Generic;

namespace MukSoft.Services.User.Query
{
    public class ReadUserQuery : IRequest<IEnumerable<LoginDto>>
    {

    }
}
