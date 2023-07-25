using System.Collections.Generic;
using System.Linq;
using System.Net;
using Charisma.Presentation.Common.Constants;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Charisma.Presentation.Controllers.Base
{
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    // [Authorize(Policy = PolicyNames.RequireDeveloperPolicy)]
    [ApiController]
    [Route("api/v{version:apiVersion}")]
    public class ApiController : ControllerBase
    {
        protected IActionResult Problem(List<Error> errors)
        {
            if (!errors.Any())
            {
                return Problem();
            }
    
            if (errors.All(error => error.Type == ErrorType.Validation))
            {
                return ValidationProblem(errors);
            }
    
            HttpContext.Items[HttpContextItemKeys.Errors] = errors;
    
            return Problem(errors[0]);
        }
    
        private IActionResult Problem(Error error)
        {
            var statusCode = error.Type switch
            {
                ErrorType.Validation => (int)HttpStatusCode.BadRequest,
                ErrorType.NotFound => (int)HttpStatusCode.NotFound,
                ErrorType.Conflict => (int)HttpStatusCode.Conflict,
                ErrorType.Failure => (int)HttpStatusCode.UnprocessableEntity,
                _ => (int)HttpStatusCode.InternalServerError
            };
    
            return Problem(statusCode: statusCode, title: error.Description);
        }
    
        private IActionResult ValidationProblem(List<Error> errors)
        {
            var modelStateDictionary = new ModelStateDictionary();
    
            foreach (var error in errors)
            {
                modelStateDictionary.AddModelError(error.Code, error.Description);
            }
    
            return ValidationProblem(modelStateDictionary);
        }
    }
}
