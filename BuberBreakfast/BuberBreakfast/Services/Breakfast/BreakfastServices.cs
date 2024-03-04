using System.Net.Http.Headers;
using BuberBreakfast.Contracts.Breakfast;
using BuberBreakFast.Models;
using BuberBreakFast.ServicesError;
using ErrorOr;
namespace BuberBreakFast.Services.Breakfast;

public class breakfastServices : IBreakfastServices
{
    private static readonly Dictionary<Guid ,Breakfastt> _breakfasts=new();
    public ErrorOr<Created> CreateBreakfast(Breakfastt breakfast)
    {
        _breakfasts.Add(breakfast.Id,breakfast);
        
        return Result.Created;
    }

    public ErrorOr<Deleted> DeleteBreakfast(Guid id)
    {
        _breakfasts.Remove(id);

        return Result.Deleted;
    }

    public ErrorOr<Breakfastt> GetBreakfast(Guid id)
    {
        if(_breakfasts.TryGetValue(id,out var breakfastt))
        {
            return breakfastt;
        }
        return Errors.Breakfast.NotFound;
    }

    public ErrorOr<UpsertedBreakfast> UpsertBreakfast(Breakfastt breakfast)
    {
        var isnew=! _breakfasts.ContainsKey(breakfast.Id);
        _breakfasts[breakfast.Id]=breakfast;

        return new UpsertedBreakfast(isnew);
    }

   
}
