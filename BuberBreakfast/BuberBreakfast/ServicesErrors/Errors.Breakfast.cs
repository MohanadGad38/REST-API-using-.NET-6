using ErrorOr;

namespace BuberBreakFast.ServicesError;
public static class Errors
{
    public static class Breakfast
    {
        public static Error NotFound =>Error.NotFound(
            code:"Breakfast not found",
            description:"Breakfast not found "
        );
    }
}