using Api.Domain.Enum;

namespace Api.Domain.Model
{
    public class PhoneModel
    {
        public PhoneType Type { get; set; }
        public int CountryCode { get; set; }
        public int AreaCode { get; set; }
        public int Prefix { get; set; }
        public int LineNumber { get; set; }
    }
}
