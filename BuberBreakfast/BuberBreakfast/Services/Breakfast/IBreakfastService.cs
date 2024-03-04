using BuberBreakFast.Models;

namespace  BuberBreakFast.Services.Breakfast;
 
 public interface IBreakfastServices
 {
    void CreateBreakfast(Breakfastt breakfast);
    void DeleteBreakfast(Guid id);
    Breakfastt GetBreakfast(Guid id);
    void UpsertBreakfast(Breakfastt breakfast);
}
