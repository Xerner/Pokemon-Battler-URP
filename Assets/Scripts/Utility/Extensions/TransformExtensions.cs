using UnityEngine;

public static class TransformExtensions
{
    /// <summary>
    /// Casts the transform as a RectTransform
    /// </summary>
    /// <param name="transform"></param>
    /// <returns></returns>
    public static RectTransform RectTransform(this Transform transform) => (RectTransform)transform;

    /// <summary>
    /// Casts the GameObjects' Transform as a RectTransform
    /// </summary>
    /// <param name="transform"></param>
    /// <returns></returns>
    public static RectTransform RectTransform(this GameObject gameObject) => (RectTransform)gameObject.transform;
}
