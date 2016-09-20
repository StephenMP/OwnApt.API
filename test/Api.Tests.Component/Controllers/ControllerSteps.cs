using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Reflection;
using Xunit;

namespace Api.Tests.Component.Controllers
{
    public class ControllerSteps
    {
        #region Protected Fields

        protected object controllerContent;
        protected IActionResult controllerResponse;

        #endregion Protected Fields

        #region Public Methods

        public void ThenICanVerifyIReceived<TModel>(HttpStatusCode statusCode)
        {
            if (statusCode == HttpStatusCode.OK)
            {
                if (typeof(TModel) != typeof(Missing))
                {
                    var content = Assert.IsType<OkObjectResult>(this.controllerResponse);
                    this.controllerContent = content.Value;
                    Assert.Equal((int)statusCode, content.StatusCode.Value);
                }
                else
                {
                    Assert.IsType<OkResult>(this.controllerResponse);
                }
            }
            else if (statusCode == HttpStatusCode.Created)
            {
                var content = Assert.IsType<CreatedResult>(this.controllerResponse);
                this.controllerContent = content.Value;
                Assert.Equal((int)statusCode, content.StatusCode.Value);
            }
        }

        #endregion Public Methods
    }
}
