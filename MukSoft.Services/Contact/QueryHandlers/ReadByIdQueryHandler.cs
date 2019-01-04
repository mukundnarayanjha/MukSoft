using AutoMapper;
using MediatR;
using MukSoft.Core.DTO;
using MukSoft.Core.Extensions;
using MukSoft.Data.Interfaces;
using MukSoft.Services.Contact.Query;
using System.Threading;
using System.Threading.Tasks;


namespace MukSoft.Services.Contact.QueryHandlers
{
    public class ReadByIdQueryHandler : IRequestHandler<ReadByIdQuery, ContactDto>
    {
        private readonly IContactService _service;
        private readonly IMapper _mapper;
        public ReadByIdQueryHandler(IContactService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<ContactDto> Handle(ReadByIdQuery request, CancellationToken cancellationToken)
        {
            request.ThrowIfNull(nameof(request));
            var contact = await _service.GetContactByIdAsync(request.Id);
            var output = _mapper.Map<ContactDto>(contact);
            return output;
        }
    }
}
