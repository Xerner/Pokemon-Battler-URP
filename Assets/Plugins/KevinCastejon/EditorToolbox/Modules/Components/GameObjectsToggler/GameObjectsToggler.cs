using UnityEngine;
using UnityEngine.Events;

namespace KevinCastejon.EditorToolbox
{
    /// <summary>
    /// Switch the enable state between two GameObjects and fire events on switching.
    /// </summary>
    public class GameObjectsToggler : MonoBehaviour
    {
        [SerializeField]
        private GameObject _a;
        [SerializeField]
        private GameObject _b;
        [SerializeField]
        private UnityEvent<GameObject> _onToggle;
        [SerializeField]
        private UnityEvent _onActivatedA;
        [SerializeField]
        private UnityEvent _onActivatedB;

        [ContextMenu("Toggle")]
        public void Toggle()
        {
            _a.SetActive(!_a.activeSelf);
            _b.SetActive(!_b.activeSelf);

            _onToggle.Invoke(_a.activeSelf ? _a : _b);

            if (_a.activeSelf)
            {
                _onActivatedA.Invoke();
            }
            else
            {
                _onActivatedB.Invoke();
            }
        }
    }
}