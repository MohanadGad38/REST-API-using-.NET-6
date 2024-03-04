using BuberBreakFast.Models;
using ErrorOr;

namespace  BuberBreakFast.Services.Breakfast;
 
 public interface IBreakfastServices
 {
    ErrorOr<Created>CreateBreakfast(Breakfastt breakfast);
    ErrorOr<Deleted> DeleteBreakfast(Guid id);
    ErrorOr<Breakfastt> GetBreakfast(Guid id);
    ErrorOr<UpsertedBreakfast> UpsertBreakfast(Breakfastt breakfast);
}
