using UnityEngine;

namespace Poke.Unity {

    [AddComponentMenu("Poke Battler/Dont Destroy On Load")]
    public class DontDestroyBehaviour : MonoBehaviour
    {
        void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
