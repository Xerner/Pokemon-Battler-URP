using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

[RequireComponent(typeof(RectTransform))]
public class Menu : PlayerInputConsumer, IPointerEnterHandler, IPointerExitHandler {
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private TabGroup tabGroup;
    [Header("Toggle Menu Bindings")]
    [SerializeField] private InputAction inputActionToggle;

    public Action<Menu> OnOpen;
    public Action<Menu> OnClose;
    public Action OnEnter;
    public Action OnExit;

    protected virtual void Start() {
        if (menuManager != null)
            menuManager.Subscribe(this);
        InitializePlayerInput(inputActionToggle, new Action<InputAction.CallbackContext>[] { Toggle });
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
