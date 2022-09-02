using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using PycApi.Data.Model;

namespace PycApi.Data.Mapping
{
    public class VehicleMap : ClassMapping<Vehicle>
    {
        public VehicleMap()
        {
            Id(x => x.Id, x =>
            {
                x.Column("id");
                x.Type(NHibernateUtil.Int32);
                x.UnsavedValue(0);
                x.Generator(Generators.Increment);
            });

            Property(b => b.name, x =>
            {
                x.Column("vehiclename");
                x.Length(50);
                x.Type(NHibernateUtil.String);
                x.NotNullable(false);

            });
            Property(b => b.plate, x =>
            {
                x.Column("vehicleplate");
                x.Length(14);
                x.Type(NHibernateUtil.String);
                x.NotNullable(false); ;

            });
            Table("vehicle");
        }
    }
}
