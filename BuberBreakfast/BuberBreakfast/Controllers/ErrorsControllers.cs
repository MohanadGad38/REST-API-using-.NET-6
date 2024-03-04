using Microsoft.AspNetCore.Mvc;

namespace BuberBreakFast.Controllers;

public class ErrorsController: ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        return Problem();

    }
}