using E_Commerce.Application.Common;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIBaseController : ControllerBase
    {
        protected ActionResult<T> HandleResult<T>(Result<T> result)
        {
            if (result.IsSuccess) return Ok(result.Value);
            return ToProblem(result.Errors);
        }
        protected ActionResult HandleResult(Result result)
        {
            if (result.IsSuccess) return NoContent();
            return ToProblem(result.Errors);
        }

        private ActionResult ToProblem(IReadOnlyList<Error> errors)
        {
            if (!errors.Any()) errors = new List<Error> { Error.Failure() };

            var error = errors.FirstOrDefault();
            var statusCode = error.Type switch
            {
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
                ErrorType.Failure => StatusCodes.Status500InternalServerError,
                _ => StatusCodes.Status500InternalServerError
            };
            var problem = ProblemDetailsFactory.CreateProblemDetails(HttpContext, statusCode, title: error.Code, detail: error.Description);
            problem.Extensions["errors"] = errors;
            return new ObjectResult(problem)
            {
                StatusCode = statusCode,
                ContentTypes = { "application/problem+json" }
                //ContentTypes = { "application/problem+json", "application/problem+xml" }
            };
        }
    }
}
