using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

[RequireComponent(typeof(RectTransform))]
public class Menu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private TabGroup tabGroup;
    [Header("Toggle Menu Bindings")]
    public InputAction InputActionToggle;

    public static readonly Vector2 Offscreen = Vector2.zero;

    Vector2 previousPosition;
    public Action<Menu> OnOpen;
    public Action<Menu> OnClose;
    public Action OnEnter;
    public Action OnExit;

    protected virtual void Start() {
        MenuManager.Instance.Subscribe(this);
    }

    protected virtual void OnDestroy() {
        MenuManager.Instance.Unsubscribe(this);
    }

    public virtual void Close() {
        OnExit?.Invoke();
        OnClose?.Invoke(this);
        gameObject.SetActive(false);
    }

    public virtual void Open() {
        OnOpen?.Invoke(this);
        gameObject.SetActive(true);
    }

    public virtual void Toggle(InputAction.CallbackContext context) => Toggle();

    public virtual void Toggle()
    {
        if (IsOpen())
        {
            Close();
        }
        else
        {
            Open();
        }
    }

    public virtual bool IsOpen()
    {
        return gameObject.activeSelf;
    }

    public void NextTab()
    {
        if (tabGroup != null)
        {
            tabGroup.NextTab();
        }
    }

    public void OnNumberKeyPress(int value)
    {
        if (tabGroup != null)
        {
            tabGroup.OnNumberKeyPress(value);
        }
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
