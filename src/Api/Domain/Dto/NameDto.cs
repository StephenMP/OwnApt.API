using System;

namespace OwnApt.Api.Domain.Dto
{
    public class NameDto : Equatable<NameDto>
    {
        #region Public Properties

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        #endregion Public Properties

        #region Public Methods

        public override int GetHashCode()
        {
            return this.FirstName.GetHashCodeSafe()
                ^ this.MiddleName.GetHashCodeSafe()
                ^ this.LastName.GetHashCodeSafe();
        }

        #endregion Public Methods
    }
}
