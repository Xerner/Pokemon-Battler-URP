using Poke.Core;
using UnityEngine;

namespace Poke.Unity
{
    [AddComponentMenu("Poke Battler/Round Timer")]
    public class RoundTimerBehaviour : MonoBehaviour
    {
        [SerializeField] TMPro.TextMeshProUGUI text;

        void Start()
        {
            HostBehaviour.Instance.Host.OnGameCreated += (Game game) =>
            {
                game.RoundTimer.OnTick += UpdateText;
            };
        }

        void UpdateText(float time)
        {
            text.text = FormatTime(time);
        }

        string FormatTime(float time)
        {
            return string.Format("{0:0.00}", time); ;
        }
    }
}
