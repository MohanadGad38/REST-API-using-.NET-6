using BuberBreakFast.Models;
namespace BuberBreakFast.Services.Breakfast;

public class breakfastServices : IBreakfastServices
{
    private static readonly Dictionary<Guid ,Breakfastt> _breakfasts=new();
    public void CreateBreakfast(Breakfastt breakfast)
    {
        _breakfasts.Add(breakfast.Id,breakfast);
        
    }

    public Breakfastt GetBreakfast(Guid id)
    {
        return _breakfasts[id];
    }
}
