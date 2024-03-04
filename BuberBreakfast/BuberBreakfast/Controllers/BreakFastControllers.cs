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
        ErrorOr<Breakfastt> requestToBreakfastResult = Breakfastt.From(request);

        if (requestToBreakfastResult.IsError)
        {
            return Problem(requestToBreakfastResult.Errors);
        }

        var breakfast = requestToBreakfastResult.Value;
        ErrorOr<Created> createBreakfastResult = _breakfastservices.CreateBreakfast(breakfast);

        return createBreakfastResult.Match(
            created => Creatednew(breakfast),
            errors => Problem(errors));

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
           ErrorOr<Breakfastt> requestToBreakfastResult = Breakfastt.From(id, request);

        if (requestToBreakfastResult.IsError)
        {
            return Problem(requestToBreakfastResult.Errors);
        }

        var breakfast = requestToBreakfastResult.Value;
        ErrorOr<UpsertedBreakfast> upsertBreakfastResult = _breakfastservices.UpsertBreakfast(breakfast);

        return upsertBreakfastResult.Match(
            upserted => upserted.isnew ? Creatednew(breakfast) : NoContent(),
            errors => Problem(errors));
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
