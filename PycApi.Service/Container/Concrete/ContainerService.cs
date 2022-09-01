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


        List<Container> IContainerService.GetContainersByVehicleId(int id)
        {
            return hibernateRepository.Where(x => x.vehicle == id).ToList();
        }
    }
}
