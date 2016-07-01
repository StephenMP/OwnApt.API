using Api.Domain.Enum;

namespace Api.Domain.Model
{
    public class AddressModel
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public State State { get; set; }
        public ZipModel Zip { get; set; }
        public string County { get; set; }
    }
}
