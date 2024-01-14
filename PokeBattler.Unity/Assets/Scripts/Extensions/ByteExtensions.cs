using UnityEngine;

namespace PokeBattler.Client.Extensions
{
    public static class ByteExtensions
    {
        public static Texture2D ToTexture2D(this byte[] bytes)
        {
            var texture = new Texture2D(2, 2);
            texture.LoadImage(bytes);
            return texture;
        }

        public static Sprite ToSprite(this byte[] bytes)
        {
            return bytes.ToSprite(Vector2.zero);
        }

        public static Sprite ToSprite(this byte[] bytes, Vector2 pivot, float ignoredAlpha = 0f)
        {
            var texture = bytes.ToTexture2D();
            var trueSize = texture.GetTrueSizeInPixels(ignoredAlpha);
            return Sprite.Create(texture, new Rect(0, 0, trueSize.x, trueSize.y), pivot);
        }

        public static bool ToTexture2D(this byte[] bytes, Texture2D texture)
        {
            return ImageConversion.LoadImage(texture, bytes);
        }
    }
}