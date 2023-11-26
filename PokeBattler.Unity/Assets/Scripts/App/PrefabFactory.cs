using System;
using UnityEngine;

namespace PokeBattler.Unity
{
    public class PrefabFactory<T> : MonoBehaviour where T : MonoBehaviour
    {
        // Reference to prefab of whatever type.
        [SerializeField]
        [Description("Only needed if this GameObject should also be a Factory")]
        private T prefab;

        public Action<T> OnCreate;
        public Action<T> OnDestroy;

        /// <summary>
        /// Creating new instance of prefab.
        /// </summary>
        /// <returns>New instance of prefab.</returns>
        public T Create()
        {
            OnCreate?.Invoke(prefab);
            return Instantiate(prefab);
        }

        /// <summary>
        /// Creating new instance of prefab at the given Transform
        /// </summary>
        /// <returns>New instance of prefab.</returns>
        public T Create(Transform transform)
        {
            OnCreate?.Invoke(prefab);
            T creation = Instantiate(prefab, transform).GetComponent<T>();
            return creation;
        }

        /// <summary>
        /// Destroy this instance
        /// </summary>
        public void Destroy()
        {
            UnityEngine.GameObject.Destroy(gameObject);
            OnDestroy?.Invoke(prefab);
        }
    }
}