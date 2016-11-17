using System;
using System.Collections.Generic;
using OwnApt.Common.Enums;

namespace OwnApt.Api.Repository.Entity.Mongo
{
    public class OwnerEntity : MongoEntity
    {
        #region Public Properties

        public DateTime Birthdate { get; set; }
        public ContactEntity Contact { get; set; }
        public ContactEntity EmergencyContact { get; set; }
        public Gender Gender { get; set; }
        public NameEntity Name { get; set; }
        public IList<string> PropertyIds { get; set; }

        #endregion Public Properties
    }
}
