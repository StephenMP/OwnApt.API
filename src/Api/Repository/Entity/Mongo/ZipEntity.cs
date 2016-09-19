using OwnApt.Common.Dto;

namespace OwnApt.Api.Repository.Entity.Mongo
{
    public class ZipEntity : Equatable
    {
        #region Public Properties

        public string Base { get; set; }
        public string Extension { get; set; }

        #endregion Public Properties
    }
}
