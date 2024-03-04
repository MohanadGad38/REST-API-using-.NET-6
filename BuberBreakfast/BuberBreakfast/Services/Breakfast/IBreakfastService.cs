using BuberBreakFast.Models;
using ErrorOr;

namespace  BuberBreakFast.Services.Breakfast;
 
 public interface IBreakfastServices
 {
    void CreateBreakfast(Breakfastt breakfast);
    void DeleteBreakfast(Guid id);
    ErrorOr<Breakfastt> GetBreakfast(Guid id);
    void UpsertBreakfast(Breakfastt breakfast);
}
