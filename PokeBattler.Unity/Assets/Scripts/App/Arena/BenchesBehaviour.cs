using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace PokeBattler.Unity
{
    [AddComponentMenu("Poke Battler/Bench")]
    public class BenchesBehaviour : MonoInstaller<BenchesBehaviour>
    {
        [HideInInspector] public List<BenchBehaviour> Benches;

        public override void Start()
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
                Benches.Add(transform.GetChild(i).GetChild(0).GetComponent<BenchBehaviour>()); ;
        }
    }
}
