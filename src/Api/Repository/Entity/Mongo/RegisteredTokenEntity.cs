using OwnApt.Common.Dto;
using System;

namespace OwnApt.Api.Repository.Entity.Mongo
{
    public class RegisteredTokenEntity : Equatable
    {
        #region Public Properties

        public string Id { get; set; }
        public string Nonce { get; set; }
        public string[] PropertyIds { get; set; }
        public string SuppliedNonce { get; set; }
        public string Token { get; set; }
        public DateTime UtcDateIssued { get; set; }

        #endregion Public Properties
    }
}
