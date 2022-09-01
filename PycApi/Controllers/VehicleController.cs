using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PycApi.Data.Model;
using PycApi.Service;
using System.Collections.Generic;

namespace PycApi.Controllers
{
    [ApiController]
    [Route("api/nhb/[controller]")]

    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService vehicleService;
        private readonly IContainerService containerService;
   

        public VehicleController(IVehicleService vehicleService, IContainerService containerService,IMapper mapper)
        {
            this.vehicleService = vehicleService;
            this.containerService = containerService;
        
        }
    

        [HttpGet("GetClustered")]
        public List<List<Container>> GetClustered(int id, int numCluster)
        {
            //Containers belongs to vehicle is fetched
            List<Container> containers = containerService.GetContainersByVehicleId(id);

            //"data" is used in cluster algorithm
            double[][] data = new double[containers.Count][];

            ////Latitude and longitude of containers are listed into "data"
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = new double[] { containers[i].latitude, containers[i].longitude };
            }

            //Datas are clustered
            //Example:
            //Assuming 3 cluster are requested
            //cluster = [0,2,2,1,2,1,0]
            //Numbers defines which cluster the latitude and longitude belongs
            int[] clusters = data.Clusterize(numCluster);

            //ClusteredContainers will be used to list containers according to their clusters
            //It has as many list of containers as numCluster
            List<List<Container>> clusteredContainers = new List<List<Container>>();
            for(int k = 0; k < numCluster; k++)
            {
                clusteredContainers.Add(new List<Container>()); //New List of container is added
            }

            //Containers are added to their cluster according to datas from clusters
            for(int j = 0; j < clusters.Length; j++)
            {
                clusteredContainers[clusters[j]].Add(containers[j]);
            } 
            return clusteredContainers;
            
        }
    }
}
