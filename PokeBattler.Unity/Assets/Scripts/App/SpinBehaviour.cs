using UnityEngine;

namespace PokeBattler.Unity
{
    public class SpinBehaviour : MonoBehaviour
    {
        public float Speed = 100f;
        public bool Enabled = false;

        void Update()
        {
            if (Enabled)
            {
                transform.Rotate(Vector3.up, Speed * Time.deltaTime);
            }
        }
    }
}
