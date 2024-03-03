using BuberBreakFast.Models;

namespace  BuberBreakFast.Services.Breakfast;
 
 public interface IBreakfastServices
 {
    void CreateBreakfast(Breakfastt breakfast);
    Breakfastt GetBreakfast(Guid id);
}
