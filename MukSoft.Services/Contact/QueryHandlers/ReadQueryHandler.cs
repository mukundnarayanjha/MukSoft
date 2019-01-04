using AutoMapper;
using MediatR;
using MukSoft.Core.DTO;
using MukSoft.Core.Extensions;
using MukSoft.Data.Interfaces;
using MukSoft.Services.Contact.Query;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MukSoft.Services.Contact.QueryHandlers
{
    public class ReadQueryHandler : IRequestHandler<ReadQuery, IEnumerable<ContactDto>>
    {
        private readonly IContactService _service;
        private readonly IMapper _mapper;
        public ReadQueryHandler(IContactService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ContactDto>> Handle(ReadQuery request, CancellationToken cancellationToken)
        {
            request.ThrowIfNull(nameof(request));
            var contactList = await _service.GetAllContactAsync();
            var output = _mapper.Map<IEnumerable<ContactDto>>(contactList);
            return output;
        }

    }
}
