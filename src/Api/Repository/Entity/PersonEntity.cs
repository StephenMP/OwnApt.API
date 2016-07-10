using OwnApt.Api.Domain.Dto;
using OwnApt.Api.Extensions;
using OwnApt.Api.Domain.Enum;

namespace OwnApt.Api.Repository.Entity
{
    public class PersonEntity : Equatable<PersonEntity>
    {
        #region Public Properties

        public int Age { get; set; }
        public ContactDto Contact { get; set; }
        public int CreditScore { get; set; }
        public Gender Gender { get; set; }
        public string Id { get; set; }
        public NameDto Name { get; set; }
        public string[] PropertyIds { get; set; }
        public PersonType Type { get; set; }

        #endregion Public Properties

        #region Public Methods

        public override int GetHashCode()
        {
            return this.Id.GetHashCodeSafe()
                ^ this.PropertyIds.GetHashCodeSafe()
                ^ this.Type.GetHashCodeSafe()
                ^ this.Name.GetHashCodeSafe()
                ^ this.Age.GetHashCodeSafe()
                ^ this.Gender.GetHashCodeSafe()
                ^ this.CreditScore.GetHashCodeSafe()
                ^ this.Contact.GetHashCodeSafe();
        }

        #endregion Public Methods
    }
}
