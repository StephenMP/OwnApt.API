using Api.Domain.Enum;

namespace Api.Repository.Entity
{
    public class PhoneEntity
    {
        public PhoneType Type { get; set; }
        public int CountryCode { get; set; }
        public int AreaCode { get; set; }
        public int Prefix { get; set; }
        public int LineNumber { get; set; }
    }
}
