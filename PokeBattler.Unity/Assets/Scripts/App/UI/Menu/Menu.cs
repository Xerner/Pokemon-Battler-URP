using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace PokeBattler.Unity {
    [RequireComponent(typeof(RectTransform))]
    public class Menu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private MenuManager menuManager;
        [SerializeField] private TabGroup tabGroup;
        [Header("Toggle Menu Bindings")]
        public InputActionReference ToggleMenu;

        public static readonly Vector2 Offscreen = Vector2.zero;

        public Action<Menu> OnOpen;
        public Action<Menu> OnClose;
        public Action OnEnter;
        public Action OnExit;

        protected virtual void Start()
        {
            MenuManager.Instance.Subscribe(this);
            Close();
            if (ToggleMenu != null) 
                ToggleMenu.action.performed += Toggle;
        }

        protected virtual void OnDestroy()
        {
            MenuManager.Instance.Unsubscribe(this);
            if (ToggleMenu != null)
                ToggleMenu.action.performed -= Toggle;
        }

        public virtual void Close()
        {
            OnExit?.Invoke();
            OnClose?.Invoke(this);
            gameObject.SetActive(false);
        }

        public virtual void Open()
        {
            OnOpen?.Invoke(this);
            gameObject.SetActive(true);
        }

        public virtual void Toggle(InputAction.CallbackContext context) => Toggle();

        public virtual void Toggle()
        {
            if (IsOpen())
                Close();
            else
                Open();
        }

        public virtual bool IsOpen()
        {
            return gameObject.activeSelf;
        }

        public void NextTab()
        {
            if (tabGroup != null)
                tabGroup.NextTab();
        }

        public void OnNumberKeyPress(int value)
        {
            if (tabGroup != null)
                tabGroup.OnNumberKeyPress(value);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnEnter?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnExit?.Invoke();
        }
    }
}
