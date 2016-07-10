using System;
using Xunit;

namespace Api.Tests.Component.Mapping
{
    public class MappingFeatures
    {
        #region Private Fields

        private MappingSteps steps = new MappingSteps();

        #endregion Private Fields

        #region Public Methods

        [Fact]
        public void CanMapPersonEntityToModel()
        {
            this.steps.GivenIHaveAPersonEntity();
            this.steps.GivenIHaveAMapper();
            this.steps.WhenIMapPersonEntityToModel();
            this.steps.ThenICanVerifyIMappedPersonSuccessfully();
        }

        [Fact]
        public void CanMapPersonModelToEntity()
        {
            this.steps.GivenIHaveAPersonModel();
            this.steps.GivenIHaveAMapper();
            this.steps.WhenIMapPersonModelToEntity();
            this.steps.ThenICanVerifyIMappedPersonSuccessfully();
        }

        [Fact]
        public void CanMapPropertyEntityToModel()
        {
            this.steps.GivenIHaveAPropertyEntity();
            this.steps.GivenIHaveAMapper();
            this.steps.WhenIMapPropertyEntityToModel();
            this.steps.ThenICanVerifyIMappedPropertySuccessfully();
        }

        [Fact]
        public void CanMapPropertyModelToEntity()
        {
            this.steps.GivenIHaveAPropertyModel();
            this.steps.GivenIHaveAMapper();
            this.steps.WhenIMapPropertyModelToEntity();
            this.steps.ThenICanVerifyIMappedPropertySuccessfully();
        }

        #endregion Public Methods
    }
}
