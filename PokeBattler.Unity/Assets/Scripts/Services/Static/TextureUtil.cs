using UnityEngine;

public class TextureUtil {
    /// <summary>
    /// Calculates the size of the texture ignoring pixels that are equal to or under the ignored alpha value
    /// </summary>
    /// <param name="ignoredAlpha">value should be from 0 to 1</param>
    /// <returns>The tallest height of the inside of the texture that has an alpha higher than the ignored alpha</returns>
    public static Vector2Int GetTrueSizeInPixels(Texture2D texture, float ignoredAlpha) {
        var heightMinMax = new MinMax();
        var widthMinMax = new MinMax();
        for (int y = 0; y < texture.height; y++) {
            for (int x = 0; x < texture.width; x++) {
                if (texture.GetPixel(y, x).a > ignoredAlpha) {
                    if (y < heightMinMax.Min) heightMinMax.Min = y;
                    if (y > heightMinMax.Max) heightMinMax.Max = y;
                    if (x < widthMinMax.Min) widthMinMax.Min = x;
                    if (x > widthMinMax.Max) widthMinMax.Max = x;
                }
            }
        }
        return new Vector2Int(heightMinMax.Max - heightMinMax.Min + 1, widthMinMax.Max - widthMinMax.Min + 1);
    }

    private class MinMax {
        public int Min = int.MaxValue;
        public int Max = 0;
    }
}
