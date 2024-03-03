using BuberBreakfast.Contracts.Breakfast;
using Microsoft.AspNetCore.Mvc;

namespace BuberBreakFast.Controllers;
[ApiController]
public class BreakfastController: ControllerBase
{
    [HttpPost("/breakfast")]
    public IActionResult CreateBreakfast(CreateBreakfastRequest request)
    {
        return Ok(request);
    }


    [HttpGet("/breakfast/{id:guid}")]
    public IActionResult Breakfast(CreateBreakfastRequest request)
    {
        return Ok(request);
    }
}
