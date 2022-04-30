using UnityEngine;

public static class Vector3Exensions {
    public static bool IsBasicallyEqualTo(this Vector3 value, Vector3 other) => Vector3.Distance(value, other).IsBasicallyZero();
}