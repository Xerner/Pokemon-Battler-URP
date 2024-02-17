using System;
using System.Collections.Generic;
using UnityEngine;

namespace PokeBattler.Common.Extensions {
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

        public static IEnumerable<Transform> GetChildren(this Transform transform)
        {
            var children = new List<Transform>();
            for (int i = 0; i < transform.childCount; i++)
            {
                children.Add(transform.GetChild(i));
            }
            return children;
        }

        public static void Select(this Transform transform, Action<Transform> predicate)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                predicate(transform.GetChild(i));
            }
        }
    }
}
