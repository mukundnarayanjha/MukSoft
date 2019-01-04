using AutoMapper;
using MediatR;
using MukSoft.Core.DTO;
using MukSoft.Core.Extensions;
using MukSoft.Data.Interfaces;
using MukSoft.Services.User.Query;
using System.Threading;
using System.Threading.Tasks;


namespace MukSoft.Services.User.QueryHandlers
{
    public class ReadByConditionUserQueryHandler : IRequestHandler<ReadByConditionUserQuery, LoginDto>
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;
        public ReadByConditionUserQueryHandler(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<LoginDto> Handle(ReadByConditionUserQuery request, CancellationToken cancellationToken)
        {
            request.ThrowIfNull(nameof(request));
            var user = await _service.GetUserByIdAsync(request.Email, request.Password);
            var output = _mapper.Map<LoginDto>(user);
            return output;
        }
    }
}
