﻿using OwnApt.Api.Domain.Enum;
using OwnApt.Api.Extensions;

namespace OwnApt.Api.Domain.Dto
{
    public class ParkingDto : Equatable<ParkingDto>
    {
        #region Public Fields + Properties

        public string Description { get; set; }
        public ParkingType Type { get; set; }

        #endregion Public Fields + Properties

        #region Public Methods

        public override int GetHashCode()
        {
            return this.Type.GetHashCodeSafe()
                ^ this.Description.GetHashCodeSafe();
        }

        #endregion Public Methods
    }
}
