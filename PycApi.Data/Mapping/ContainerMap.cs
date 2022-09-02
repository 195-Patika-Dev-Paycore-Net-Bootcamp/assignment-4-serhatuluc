using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using PycApi.Data.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PycApi.Data.Mapping
{
    public class ContainerMap : ClassMapping<Model.Container>
    {
        //Mapping of container. Double type is preferred for latitude and longitude
        public ContainerMap()
        {
            Id(x => x.Id, x =>
            {
                x.Column("id");
                x.Type(NHibernateUtil.Int32);
                x.UnsavedValue(0);
                x.Generator(Generators.Increment);
            });

            Property(b => b.containerName, x =>
            {
                x.Column("containername");
                x.Length(50);
                x.Type(NHibernateUtil.String);
                x.NotNullable(false);

            });
            Property(b => b.latitude, x =>
            {
                x.Column("latitude");
                x.Type(NHibernateUtil.Double);
                x.NotNullable(false); ;

            });
            Property(b => b.longitude, x =>
            {
                x.Column("longitude");
                x.Type(NHibernateUtil.Double);
                x.NotNullable(false); ;

            });
            Property(b => b.vehicle, x =>
            {
                x.Column("vehicleid");
                x.Type(NHibernateUtil.Int32);
                x.NotNullable(true); ;

            });
            Table("containers");
        }
    }
}

