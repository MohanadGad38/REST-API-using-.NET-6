using ErrorOr;

namespace BuberBreakFast.ServicesError;
public static class Errors
{
    public static class Breakfast
    {
        public static Error InvalidName => Error.Validation(
            code: "Breakfast.InvalidName",
            description: $"Breakfast name must be at least {Models.Breakfastt.MinNameLength}" +
                $" characters long and at most {Models.Breakfastt.MaxNameLength} characters long.");

        public static Error InvalidDescription => Error.Validation(
            code: "Breakfast.InvalidDescription",
            description: $"Breakfast description must be at least {Models.Breakfastt.MinDescriptionLength}" +
                $" characters long and at most {Models.Breakfastt.MaxDescriptionLength} characters long.");

        public static Error NotFound => Error.NotFound(
            code: "Breakfast.NotFound",
            description: "Breakfast not found");
    }
}