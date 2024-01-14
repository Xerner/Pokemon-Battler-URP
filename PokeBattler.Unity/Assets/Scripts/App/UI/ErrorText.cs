using UnityEngine;

namespace PokeBattler.Unity
{
    [AddComponentMenu("Poke Battler/Error Text")]
    [RequireComponent(typeof(TMPro.TextMeshProUGUI))]
    public class ErrorTextBehaviour : MonoBehaviour
    {
        TMPro.TextMeshProUGUI text;
        [SerializeField] Color errorColor = new Color(255, 64, 64, 1);
        [SerializeField] Color warningColor = new Color(255, 255, 64, 1);

        public Color ErrorColor { get => errorColor; }
        public Color WarningColor { get => warningColor; }

        void Start()
        {
            text = GetComponent<TMPro.TextMeshProUGUI>();
        }

        public void Error(string message)
        {
            text.text = message;
            text.color = ErrorColor;
        }
        
        public void Warning(string message)
        {
            text.text = message;
            text.color = WarningColor;
        }
    }
}
