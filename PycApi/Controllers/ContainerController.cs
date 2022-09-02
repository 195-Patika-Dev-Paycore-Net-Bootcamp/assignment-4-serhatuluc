using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PycApi.Base;
using PycApi.Data.Model;
using PycApi.Service;
using System.Collections.Generic;

namespace PycApi.Controllers
{
    [ApiController]
    [Route("api/nhb/[controller]")]

    public class ContainerController : ControllerBase
    {
        private readonly IContainerService containerService;
   

        public ContainerController(IVehicleService vehicleService, IContainerService containerService,IMapper mapper)
        {
            this.containerService = containerService;

        }
    

        [HttpGet("GetClustered")]
        public BaseResponse<List<List<Container>>> GetClustered(int id, int numCluster)
        {
            
            return containerService.Clusterized(id,numCluster);
            
        }
    }
}
