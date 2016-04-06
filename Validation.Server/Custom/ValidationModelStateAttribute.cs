using Autofac;
using FluentValidation;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Validation.Shared;

namespace Validation.Server.Custom
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class ValidateModelStateAttribute : ActionFilterAttribute
    {
        private string _argName;
        private Type _modelValidationType;

        public ValidateModelStateAttribute(Type modelValidationType, string argName = "item")
        {
            _modelValidationType = modelValidationType;
            _argName = argName;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to skip validation (false by default).
        /// </summary>
        /// <remarks>
        /// Allows overriding the default behaviour on an individual action/controller if an instance
        /// is already registered in the global filters.
        /// </remarks>
        public bool SkipValidation { get; set; }

        /// <summary>
        /// Occurs before the action method is invoked.
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (SkipValidation || actionContext == null)
            {
                return;
            }

            if (actionContext.ActionArguments.ContainsKey(_argName))
            {
                var model = actionContext.ActionArguments["item"];

                //check for custom validation as well
                IValidator validator;
                object instance;
                if (_modelValidationType != null && Wibci.IoC.TryResolveNamed(_modelValidationType.Name, typeof(IValidator), out instance))
                {
                    validator = (IValidator)instance;
                    var validationResult = validator.Validate(model);

                    if (!validationResult.IsValid)
                    {
                        foreach (var error in validationResult.Errors)
                        {
                            actionContext.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                        }
                    }
                }
            }

            if (!actionContext.ModelState.IsValid)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, actionContext.ModelState);
            }
        }
    }
}