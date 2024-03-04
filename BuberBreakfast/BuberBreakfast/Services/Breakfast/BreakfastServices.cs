using BuberBreakFast.Models;
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

    public Breakfastt GetBreakfast(Guid id)
    {
        return _breakfasts[id];
    }

    public void UpsertBreakfast(Breakfastt breakfast)
    {
        _breakfasts[breakfast.Id]=breakfast;
    }
}
