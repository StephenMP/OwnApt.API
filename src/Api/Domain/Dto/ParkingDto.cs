using System;
using OwnApt.Api.Domain.Enum;

namespace OwnApt.Api.Domain.Dto
{
    public class ParkingDto : Equatable<ParkingDto>
    {
        #region Public Properties

        public string Description { get; set; }
        public ParkingType Type { get; set; }

        #endregion Public Properties

        #region Public Methods

        public override int GetHashCode()
        {
            return this.Type.GetHashCodeSafe()
                ^ this.Description.GetHashCodeSafe();
        }

        #endregion Public Methods
    }
}
