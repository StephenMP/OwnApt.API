using OwnApt.Api.Domain.Enum;

namespace OwnApt.Api.Domain.Model
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
