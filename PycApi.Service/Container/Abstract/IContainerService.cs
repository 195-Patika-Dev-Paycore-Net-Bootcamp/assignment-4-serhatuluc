using PycApi.Data;
using PycApi.Data.Model;
using PycApi.Dto;
using System.Collections.Generic;

namespace PycApi.Service
{
    public interface IContainerService  : IBaseService<ContainerDto, Container>
    {
        List<Container> GetContainersByVehicleId(int id);
    }
}
