using OwnApt.Common.Dto;
using OwnApt.Common.Enums;

namespace OwnApt.Api.Repository.Entity.Mongo
{
    public class AddressEntity : Equatable
    {
        #region Public Properties

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public State State { get; set; }
        public ZipEntity Zip { get; set; }

        #endregion Public Properties
    }
}
