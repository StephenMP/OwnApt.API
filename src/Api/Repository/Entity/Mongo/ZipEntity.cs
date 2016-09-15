using OwnApt.Common.Dto;

namespace OwnApt.Api.Repository.Entity.Mongo
{
    public class ZipEntity : Equatable
    {
        #region Properties

        public string Base { get; set; }
        public string Extension { get; set; }

        #endregion Properties
    }
}
