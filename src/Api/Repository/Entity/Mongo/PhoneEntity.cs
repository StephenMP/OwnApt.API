using OwnApt.Common.Dto;
using OwnApt.Common.Enums;

namespace OwnApt.Api.Repository.Entity.Mongo
{
    public class PhoneEntity : Equatable
    {
        #region Public Properties

        public int AreaCode { get; set; }
        public int CountryCode { get; set; }
        public int LineNumber { get; set; }
        public int Prefix { get; set; }
        public PhoneType Type { get; set; }

        #endregion Public Properties
    }
}
