using System;
using OwnApt.Api.Extensions;

namespace OwnApt.Api.Domain.Dto
{
    public class ContactDto : Equatable<ContactDto>
    {
        #region Public Properties

        public string Email { get; set; }
        public PhoneDto[] Phones { get; set; }

        #endregion Public Properties

        #region Public Methods

        public override int GetHashCode()
        {
            return this.Email.GetHashCodeSafe()
                 ^ this.Phones.GetHashCodeSafe();
        }

        #endregion Public Methods
    }
}
