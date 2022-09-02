
namespace PycApi.Dto
{
    public class ContainerDto
    {
        public virtual int Id { get; set; }
        public virtual string containerName { get; set; }
        public virtual double latitude { get; set; }
        public virtual double longitude { get; set; }
        public virtual int vehicle { get; set; }
    }
}
