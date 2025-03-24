using AutoChess.Contracts.Enums;

namespace AutoChess.Contracts.Extensions
{
    public static class EContainerTypeExtensions
    {
        public static bool Is(this EContainerType tag, EContainerType otherTag)
        {
            return (tag & otherTag) == otherTag;
        }
    }
}
