using AutoChess.Contracts.Enums;

namespace AutoChess.Contracts.Extensions
{
    public static class EContainerTypeExtensions
    {
        public static bool Is(this EContainerTag tag, EContainerTag otherTag)
        {
            return (tag & otherTag) == otherTag;
        }
    }
}
