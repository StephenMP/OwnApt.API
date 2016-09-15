﻿using OwnApt.Common.Dto;
using OwnApt.Common.Enum;
using System.Collections.Generic;

namespace OwnApt.Api.Repository.Entity.Mongo
{
    public class OwnerEntity : Equatable
    {
        #region Properties

        public BirthdateEntity Birthdate { get; set; }
        public ContactEntity Contact { get; set; }
        public ContactEntity EmergencyContact { get; set; }
        public Gender Gender { get; set; }
        public string Id { get; set; }
        public NameEntity Name { get; set; }
        public IList<string> PropertyIds { get; set; }

        #endregion Properties
    }
}
