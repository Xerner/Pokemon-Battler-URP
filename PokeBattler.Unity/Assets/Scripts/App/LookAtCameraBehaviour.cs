using PokeBattler.Core;
using UnityEngine;

namespace PokeBattler.Unity
{
    [AddComponentMenu("Poke Battler/Look At Camera")]
    public class LookAtCameraBehaviour : MonoBehaviour
    {
        public bool Enabled = true;

        void Update()
        {
            if (Enabled) 
                transform.rotation = Camera.main.CameraRig().transform.rotation;
        }
    }
}
