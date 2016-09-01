using OwnApt.Api.Domain.Enum;
using OwnApt.Api.Extensions;

namespace OwnApt.Api.Domain.Dto
{
    public class PhoneDto : Equatable<PhoneDto>
    {
        #region Public Properties

        public int AreaCode { get; set; }
        public int CountryCode { get; set; }
        public int LineNumber { get; set; }
        public int Prefix { get; set; }
        public PhoneType Type { get; set; }

        #endregion Public Properties

        #region Public Methods

        public override int GetHashCode()
        {
            return this.Type.GetHashCodeSafe()
                ^ this.CountryCode.GetHashCodeSafe()
                ^ this.AreaCode.GetHashCodeSafe()
                ^ this.Prefix.GetHashCodeSafe()
                ^ this.LineNumber.GetHashCodeSafe();
        }

        #endregion Public Methods
    }
}
