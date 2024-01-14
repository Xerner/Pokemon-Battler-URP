namespace PokeBattler.Common.Models
{
    public class Vector2Int
    {
        public int x;
        public int y;

        public Vector2Int(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2Int Zero
        {
            get => new(0, 0);
        }
    }
}
