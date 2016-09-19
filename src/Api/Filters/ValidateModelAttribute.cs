using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Text;

namespace OwnApt.Api.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class ValidateModelAttribute : ActionFilterAttribute
    {
        #region Public Methods

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            foreach (var argument in context.ActionArguments)
            {
                if (argument.Value == null)
                {
                    context.ModelState.AddModelError(argument.Key, $"{argument.Key} cannot be null");
                }
            }

            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }

        #endregion Public Methods
    }
}
