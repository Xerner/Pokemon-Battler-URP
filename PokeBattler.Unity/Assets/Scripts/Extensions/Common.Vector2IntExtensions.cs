namespace PokeBattler.Client.Extensions
{
    public static class Common_Vector2IntExtensions
    {
        public static UnityEngine.Vector2Int ToUnity(this Common.Models.Vector2Int vector)
        {
            return new UnityEngine.Vector2Int(vector.x, vector.y);
        }
    }
}