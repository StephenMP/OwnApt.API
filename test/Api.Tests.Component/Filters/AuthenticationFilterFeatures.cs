using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Component.Filters
{
    public class AuthenticationFilterFeatures
    {
        private AuthenticationFilterSteps steps = new AuthenticationFilterSteps();

        [Fact(Skip = "Not implemented")]
        public void CanAuthenticate()
        {

        }

        [Fact(Skip = "Not implemented")]
        public void CanAuthenticateWithBody()
        {

        }

        [Fact(Skip = "Not implemented")]
        public void CannotAuthenticateDueToNoAuthHeader()
        {

        }

        [Fact(Skip = "Not implemented")]
        public void CannotAuthenticateDueToBadScheme()
        {

        }

        [Fact(Skip = "Not implemented")]
        public void CannotAuthenticateDueToIncorrectParameterSplitLength()
        {

        }

        [Fact(Skip = "Not implemented")]
        public void CannotAuthenticateDueToOutdatedTimestamp()
        {

        }

        [Fact(Skip = "Not implemented")]
        public void CannotAuthenticateDueToUnrecognizedAppId()
        {

        }

        [Fact(Skip = "Not implemented")]
        public void CannotAuthenticateDueToBadSignedSecretKey()
        {

        }

        [Fact(Skip = "Not implemented")]
        public void CannotAuthenticateDueToBadSignedBody()
        {

        }
    }
}
