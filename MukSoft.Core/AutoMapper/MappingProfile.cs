using AutoMapper;
using MukSoft.Core.Domain;
using MukSoft.Core.DTO;
namespace MukSoft.Core.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ContactDto, Contact>().ReverseMap();
            CreateMap<LoginDto, User>().ReverseMap();

            CreateMap<EmployeeDto, Employee>()
                .ForMember(dest => dest.Address, opts => opts.MapFrom(src => new Address { City = src.City, State = src.State }));

        }
    }
}
