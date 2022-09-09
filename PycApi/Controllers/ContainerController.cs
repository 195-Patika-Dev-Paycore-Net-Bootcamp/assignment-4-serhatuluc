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
        public ActionResult<BaseResponse<List<List<Container>>>> GetClustered(int id, int numCluster)
        {
            //Containers belongs to vehicle is fetched
            List<Container> containers = containerService.GetContainersByVehicleId(id);

            if (containers.Count == 0)
            {
                return NotFound("Vehicle is not found");
            }

            if(containers.Count < numCluster)
            {
                return BadRequest("Maximum number of cluster is excessed");
            }
            return containerService.Clusterized(id,numCluster,containers);
            
        }
    }
}
