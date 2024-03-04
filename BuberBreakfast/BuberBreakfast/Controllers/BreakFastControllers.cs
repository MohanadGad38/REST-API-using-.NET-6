using BuberBreakfast.Contracts.Breakfast;
using BuberBreakFast.Models;
using BuberBreakFast.Services.Breakfast;
using BuberBreakFast.ServicesError;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BuberBreakFast.Controllers;

public class BreakfastController: ApiController
{
    private readonly IBreakfastServices _breakfastservices;
    public BreakfastController(IBreakfastServices breakfastServices)
    {
        _breakfastservices=breakfastServices;
    }
    [HttpPost("/breakfast")]
    public IActionResult CreateBreakfast(CreateBreakfastRequest request)
    {
        var breakfast = new Breakfastt(
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
        ErrorOr<Created> createBreakfastResult = _breakfastservices.CreateBreakfast(breakfast);
        //done

       return createBreakfastResult.Match(
        created=>Creatednew(breakfast),
        errors=>Problem(errors)
       );

    }

    [HttpGet("/breakfast/{id:guid}")]
    public IActionResult GetBreakfast(Guid id)
    {
        ErrorOr<Breakfastt> getBreakfastResult = _breakfastservices.GetBreakfast(id);
        return getBreakfastResult.Match(
            breakfastt=>Ok(MapBreakfastResponse(breakfastt)),
            errors=>Problem(errors)
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
       ErrorOr<UpsertedBreakfast> upserResult= _breakfastservices.UpsertBreakfast(breakfast);
        return upserResult.Match(
            upserted=>upserted.isnew?Creatednew(breakfast): NoContent(),
            errors =>Problem(errors)
        );
    }

      [HttpDelete("/breakfast/{id:guid}")]
    public IActionResult DeleteBreakfast(Guid id)
    {
        ErrorOr<Deleted> deleteresult=_breakfastservices.DeleteBreakfast(id);
        return deleteresult.Match(
            deleted=>NoContent(),
            errors=> Problem(errors));
    }
       private IActionResult Creatednew(Breakfastt breakfast)
    {
        return CreatedAtAction(
                 actionName:   nameof(GetBreakfast),
                 routeValues:   new { id = breakfast.Id },
                    value: MapBreakfastResponse(breakfast));
    }
}
