﻿using OwnApt.Common.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OwnApt.Api.Repository.Entity.Mongo
{
    public class RegisteredTokenEntity : Equatable
    {
        #region Public Properties

        public string Token { get; set; }
        public string Id { get; set; }
        public string Nonce { get; set; }
        public string[] PropertyIds { get; set; }
        public string SuppliedNonce { get; set; }
        public DateTime UtcDateIssued { get; set; }

        #endregion Public Properties
    }
}
