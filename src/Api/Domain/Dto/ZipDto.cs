using OwnApt.Api.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OwnApt.Api.Domain.Dto
{
    public class ZipDto : Equatable<ZipDto>
    {
        #region Public Fields + Properties

        public string Base { get; set; }
        public string Extension { get; set; }

        #endregion Public Fields + Properties

        #region Public Methods

        public override int GetHashCode()
        {
            return this.Base.GetHashCodeSafe()
                ^ this.Extension.GetHashCodeSafe();
        }

        #endregion Public Methods
    }
}
