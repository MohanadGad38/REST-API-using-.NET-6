using BuberBreakFast.Models;
using BuberBreakFast.ServicesError;
using ErrorOr;
namespace BuberBreakFast.Services.Breakfast;

public class breakfastServices : IBreakfastServices
{
    private static readonly Dictionary<Guid ,Breakfastt> _breakfasts=new();
    public void CreateBreakfast(Breakfastt breakfast)
    {
        _breakfasts.Add(breakfast.Id,breakfast);
        
    }

    public void DeleteBreakfast(Guid id)
    {
        _breakfasts.Remove(id);
    }

    public ErrorOr<Breakfastt> GetBreakfast(Guid id)
    {
        if(_breakfasts.TryGetValue(id,out var breakfastt))
        {
            return breakfastt;
        }
        return Errors.Breakfast.NotFound;
    }

    public void UpsertBreakfast(Breakfastt breakfast)
    {
        _breakfasts[breakfast.Id]=breakfast;
    }

   
}
