using PokeBattler.Common;
using PokeBattler.Common.Extensions;
using UnityEngine;

namespace PokeBattler.Unity
{
    [RequireComponent(typeof(RectTransform))]
    public class ResourceBar : MonoBehaviour
    {
        public float TotalResource = 100;
        float currentResource;
        float originalWidth;

        public float Percentage { get { return currentResource / TotalResource; } }
        public float Total { get { return TotalResource; } }
        public float Current { get { return currentResource; } }

        private void Start()
        {
            originalWidth = transform.RectTransform().rect.width;
        }

        public void Set(float newCurrent)
        {
            currentResource = newCurrent;
            Resize();
        }

        public void Reduce(float reduction)
        {
            currentResource -= reduction;
            Resize();
        }

        public void Restore(float restore)
        {
            currentResource += restore;
            Resize();
        }

        void Resize()
        {
            transform.RectTransform().sizeDelta = new Vector2(originalWidth * Percentage, transform.RectTransform().rect.height);
        }
    }
}
