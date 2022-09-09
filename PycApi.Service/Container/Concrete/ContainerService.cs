using AutoMapper;
using NHibernate;
using PycApi.Base;
using PycApi.Data;
using PycApi.Data.Model;
using PycApi.Dto;
using System.Collections.Generic;
using System.Linq;

namespace PycApi.Service
{
    public class ContainerService : BaseService<ContainerDto, Container>, IContainerService
    {
        protected readonly ISession session;
        protected readonly IMapper mapper;
        protected readonly IHibernateRepository<Container> hibernateRepository;

        public ContainerService(ISession session, IMapper mapper) : base(session, mapper)
        {
            this.session = session;
            this.mapper = mapper;

            hibernateRepository = new HibernateRepository<Container>(session);
        }

        public override BaseResponse<IEnumerable<ContainerDto>> GetAll()
        {
            return base.GetAll();
        }

        public override BaseResponse<ContainerDto> GetById(int id)
        {
             return base.GetById(id);
        }


       public List<Container> GetContainersByVehicleId(int id)
        {
            return hibernateRepository.Where(x => x.vehicle == id).ToList();
        }

        public BaseResponse<List<List<Container>>> Clusterized(int id, int numCluster,List<Container> containers)
        {
           
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
            for (int k = 0; k < numCluster; k++)
            {
                clusteredContainers.Add(new List<Container>()); //New List of container is added
            }

            //Containers are added to their cluster according to datas from clusters
            for (int j = 0; j < clusters.Length; j++)
            {
                clusteredContainers[clusters[j]].Add(containers[j]);
            }
        
            return new BaseResponse<List<List<Container>>>(clusteredContainers);
        }

    }
}
