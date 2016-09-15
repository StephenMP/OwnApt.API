﻿using OwnApt.Common.Dto;

namespace OwnApt.Api.Repository.Entity.Mongo
{
    public class AmenityEntity : Equatable
    {
        #region Properties

        public string Description { get; set; }
        public string Type { get; set; }

        #endregion Properties
    }
}