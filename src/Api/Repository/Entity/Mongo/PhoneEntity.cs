using OwnApt.Common.Dto;
using OwnApt.Common.Enum;

namespace OwnApt.Api.Repository.Entity.Mongo
{
    public class PhoneEntity : Equatable
    {
        #region Properties

        public int AreaCode { get; set; }
        public int CountryCode { get; set; }
        public int LineNumber { get; set; }
        public int Prefix { get; set; }
        public PhoneType Type { get; set; }

        #endregion Properties
    }
}
