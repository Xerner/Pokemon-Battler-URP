using System.Collections.Generic;
using UnityEngine;

namespace PokeBattler.Unity
{
    [AddComponentMenu("Poke Battler/Bench")]
    public class BenchesBehaviour : MonoBehaviour
    {
        [HideInInspector] public List<BenchBehaviour> Benches;

        void Start()
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
                Benches.Add(transform.GetChild(i).GetChild(0).GetComponent<BenchBehaviour>()); ;
        }

        public BenchBehaviour GetAvailableBench()
        {
            foreach (BenchBehaviour bench in Benches)
                if (bench.Pokemon == null)
                    return bench;
            return null;
        }
    }
}
