using AutoMapper;
using NHibernate;
using PycApi.Base;
using PycApi.Data;
using PycApi.Data.Model;
using PycApi.Dto;
using System.Collections.Generic;
using System.Linq;

namespace PycApi.Service.Concrete
{
    public class VehicleService : BaseService<VehicleDto, Vehicle>, IVehicleService
    {
        protected readonly ISession session;
        protected readonly IMapper mapper;
        protected readonly IHibernateRepository<Vehicle> hibernateRepository;

        public VehicleService(ISession session, IMapper mapper) : base(session, mapper)
        {
            this.session = session;
            this.mapper = mapper;

            hibernateRepository = new HibernateRepository<Vehicle>(session);
        }

        public override BaseResponse<IEnumerable<VehicleDto>> GetAll()
        {
            return base.GetAll();
        }


        public override BaseResponse<VehicleDto> GetById(int id)
        {
             return base.GetById(id);
        }


    }
}
