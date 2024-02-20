using System;
using UnityEngine;

namespace PokeBattler.Unity
{
    public class PrefabFactory : ScriptableObject
    {
        [SerializeField]
        GameObject prefab;

        void OnValidate()
        {
            if (prefab == null)
            {
                throw new NullReferenceException("Factory prefab is null");
            }   
        }

        /// <summary>
        /// Creating new instance of prefab.
        /// </summary>
        /// <returns>New instance of prefab.</returns>
        public GameObject Create()
        {
            var prefabFactory = Instantiate(prefab);
            return Created(prefabFactory);
        }

        /// <summary>
        /// Creating new instance of prefab at the given Transform
        /// </summary>
        /// <returns>New instance of prefab.</returns>
        public GameObject Create(Transform transform)
        {
            var prefabFactory = Instantiate(prefab, transform);
            return Created(prefabFactory);
        }

        GameObject Created(GameObject prefabFactory)
        {
            return prefabFactory;
        }
    }
}
