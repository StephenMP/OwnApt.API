using OwnApt.Api.Domain.Enum;
using OwnApt.Api.Extensions;

namespace OwnApt.Api.Domain.Dto
{
    public class AddressDto : Equatable<AddressDto>
    {
        #region Public Fields + Properties

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public State State { get; set; }
        public ZipDto Zip { get; set; }

        #endregion Public Fields + Properties

        #region Public Methods

        public override int GetHashCode()
        {
            return this.Address1.GetHashCodeSafe()
                       ^ this.Address2.GetHashCodeSafe()
                       ^ this.City.GetHashCodeSafe()
                       ^ this.County.GetHashCodeSafe()
                       ^ this.State.GetHashCodeSafe()
                       ^ this.Zip.GetHashCodeSafe();
        }

        #endregion Public Methods
    }
}
