using AutoMapper;
using MediatR;
using MukSoft.Core.DTO;
using MukSoft.Core.Extensions;
using MukSoft.Data.Interfaces;
using MukSoft.Services.User.Query;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MukSoft.Services.User.QueryHandlers
{

    public class ReadUserQueryHandler : IRequestHandler<ReadUserQuery, IEnumerable<LoginDto>>
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;
        public ReadUserQueryHandler(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LoginDto>> Handle(ReadUserQuery request, CancellationToken cancellationToken)
        {
            request.ThrowIfNull(nameof(request));
            var userList = await _service.GetAllUserAsync();
            var output = _mapper.Map<IEnumerable<LoginDto>>(userList);
            return output;
        }
    }
}
