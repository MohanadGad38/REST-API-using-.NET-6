using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BuberBreakFast.Controllers;
[ApiController]
public class ApiController:ControllerBase
{

    protected IActionResult Problem(List<Error> errors)
    {
        var FirstError=errors[0];
        var statusCode=FirstError.Type switch
        {
            ErrorType.NotFound =>StatusCodes.Status404NotFound,
            ErrorType.Validation =>StatusCodes.Status400BadRequest,
            ErrorType.Conflict=>StatusCodes.Status409Conflict,
            _=>StatusCodes.Status500InternalServerError

        };
        return Problem(statusCode:statusCode,title:FirstError.Description);
    }
}