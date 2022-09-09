using PycApi.Base;
using PycApi.Data;
using PycApi.Data.Model;
using PycApi.Dto;
using System.Collections.Generic;

namespace PycApi.Service
{
    public interface IContainerService  : IBaseService<ContainerDto, Container>
    {
        BaseResponse<List<List<Container>>> Clusterized(int id,int numCluster,List<Container> containers);
        List<Container> GetContainersByVehicleId(int id);

    }
}
