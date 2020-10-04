using AutoMapper;
using DTO.WebApi;
using Entities.Concrete;

namespace Business.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Users, UserAll>().ReverseMap();
            CreateMap<Users, UserGet>().ReverseMap();
            CreateMap<User, Users > ().ReverseMap();
        }
    }
}
