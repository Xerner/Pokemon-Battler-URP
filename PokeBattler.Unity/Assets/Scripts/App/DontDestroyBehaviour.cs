using UnityEngine;

namespace PokeBattler.Unity {

    [AddComponentMenu("Poke Battler/Dont Destroy On Load")]
    public class DontDestroyBehaviour : MonoBehaviour
    {
        void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
