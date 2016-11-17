using System.Collections.Generic;
using OwnApt.Common.Dto;

namespace OwnApt.Api.Repository.Entity.Mongo
{
    public class ContactEntity : Equatable
    {
        #region Public Properties

        public string Email { get; set; }
        public AddressEntity HomeAddress { get; set; }
        public IList<PhoneEntity> Phones { get; set; }

        #endregion Public Properties
    }
}
