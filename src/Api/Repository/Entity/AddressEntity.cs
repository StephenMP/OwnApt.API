using OwnApt.Api.Domain.Enum;

namespace OwnApt.Api.Repository.Entity
{
    public class AddressEntity
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public State State { get; set; }
        public ZipEntity Zip { get; set; }
        public string County { get; set; }
    }
}
