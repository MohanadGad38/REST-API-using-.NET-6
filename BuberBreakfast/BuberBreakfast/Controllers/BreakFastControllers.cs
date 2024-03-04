using BuberBreakfast.Contracts.Breakfast;
using BuberBreakFast.Models;
using BuberBreakFast.Services.Breakfast;
using BuberBreakFast.ServicesError;
using ErrorOr;
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
        ErrorOr<Breakfastt> getBreakfastResult = _breakfastservices.GetBreakfast(id);
        return getBreakfastResult.Match(
            breakfastt=>Ok(MapBreakfastResponse(breakfastt)),
            Errors=>Problem()
        );
        // if (getBreakfastResult.IsError && getBreakfastResult.FirstError == Errors.Breakfast.NotFound)
        // {
        //     return NotFound();
        // }

        // var breakfastt = getBreakfastResult.Value;

        // BreakfastResponse respones = MapBreakfastResponse(breakfastt);
        // return Ok(respones);
    }

    private static BreakfastResponse MapBreakfastResponse(Breakfastt breakfastt)
    {
        return new BreakfastResponse(
                    breakfastt.Id,
                    breakfastt.Name,
                    breakfastt.Description,
                    breakfastt.StartDateTime,
                    breakfastt.EndDateTime,
                    breakfastt.LastModifiedDateTime,
                    breakfastt.Savory,
                    breakfastt.Sweet
                );
    }

    [HttpPut("/breakfast/{id:guid}")]
    public IActionResult UpsertBreakfast(Guid id,UpsertBreakfastRequest request)
    {
          var breakfast= new Breakfastt(
            id,
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            DateTime.UtcNow,
            request.Savory,
            request.Sweet
        );
        _breakfastservices.UpsertBreakfast(breakfast);
        return NoContent();
    }

      [HttpDelete("/breakfast/{id:guid}")]
    public IActionResult DeleteBreakfast(Guid id)
    {
        _breakfastservices.DeleteBreakfast(id);
        return NoContent();
    }
}
