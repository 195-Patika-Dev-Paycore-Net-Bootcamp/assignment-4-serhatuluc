using AutoMapper;
using PycApi.Data.Model;
using PycApi.Dto;

namespace PycApi.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Vehicle, VehicleDto>();
            CreateMap<VehicleDto, Vehicle>();
            CreateMap<ContainerDto, Container>();
            CreateMap<Container, ContainerDto>();
        }

    }
}
