using BuberBreakfast.Contracts.Breakfast;
using BuberBreakFast.Models;
using BuberBreakFast.Services.Breakfast;
using Microsoft.AspNetCore.Mvc;

namespace BuberBreakFast.Controllers;
[ApiController]
public class BreakfastController: ControllerBase
{
    private readonly IBreakfastServices _breakfastservices;
    public BreakfastController(IBreakfastServices breakfastServices)
    {
        _breakfastservices=breakfastServices;
    }
    [HttpPost("/breakfast")]
    public IActionResult CreateBreakfast(CreateBreakfastRequest request)
    {
        var breakfast= new Breakfastt(
            Guid.NewGuid(),
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            DateTime.UtcNow,
            request.Savory,
            request.Sweet
        );


        //db
        _breakfastservices.CreateBreakfast(breakfast);
        //done
        var respones=new BreakfastResponse(
            breakfast.Id,
            breakfast.Name,
            breakfast.Description,
            breakfast.StartDateTime,
            breakfast.EndDateTime,
            breakfast.LastModifiedDateTime,
            breakfast.Savory,
            breakfast.Sweet
        );
        return CreatedAtAction(
            nameof(GetBreakfast),
            new{id =breakfast.Id},
            value :respones);
    }


    [HttpGet("/breakfast/{id:guid}")]
    public IActionResult GetBreakfast(Guid id)
    {
        
        return Ok(id);
    }

     [HttpPut("/breakfast/{id:guid}")]
    public IActionResult UpsertBreakfast(Guid id,UpsertBreakfastRequest request)
    {
        return Ok(id);
    }

      [HttpDelete("/breakfast/{id:guid}")]
    public IActionResult DeleteBreakfast(Guid id)
    {
        return Ok(id);
    }
}
