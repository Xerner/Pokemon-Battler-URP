using Poke.Core;
using UnityEngine;

namespace Poke.Unity
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
