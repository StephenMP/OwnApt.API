using OwnApt.Common.Dto;
using OwnApt.Common.Enum;

namespace OwnApt.Api.Repository.Entity.Mongo
{
    public class BirthdateEntity : Equatable
    {
        #region Properties

        public int Day { get; set; }
        public Month Month { get; set; }
        public int Year { get; set; }

        #endregion Properties
    }
}
