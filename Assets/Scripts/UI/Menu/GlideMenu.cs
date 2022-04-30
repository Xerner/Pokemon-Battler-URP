using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GlideMenu : Menu
{
    public bool DeactivateOnClose = true;
    [SerializeField]
    private MenuPosition menuPosition = MenuPosition.Bottom;
    [SerializeField]
    private Canvas Canvas;
    private Rect menuBounds;
    private Vector2 closedPosition;
    private Vector2 openPosition;
    private bool isOpen;
    private float GlideTime = 0.5f;

    protected new void Start()
    {
        // Assumed to be used in child classes for use in movement calculations
        menuBounds = gameObject.GetComponent<RectTransform>().rect;
        // 
        openPosition = transform.position;
        closedPosition = CalculateClosedPosition();
        base.Start();
        InstantlyClose();
    }

    /// <summary>If needed, glides the menu into or out of place</summary>
    private void Update()
    {
        // Turn off when we reach our destination
        if (DeactivateOnClose && BasicallyAtClosedPosition()) gameObject.SetActive(false);
    }

    /// <summary>Opens the menu</summary>
    public override void Open() {
        isOpen = true;
        OnOpen?.Invoke(this);
        if (!gameObject.activeSelf) gameObject.SetActive(true);
        LeanTween.move(gameObject, openPosition, GlideTime).setEaseOutCubic();
    }

    public override bool IsOpen()
    {
        return isOpen;
    }

    public bool IsVisible()
    {
        return transform.position.IsBasicallyEqualTo(openPosition);
    }

    /// <summary>Closes the menu</summary>
    public override void Close()
    {
        isOpen = false;
        OnClose?.Invoke(this);
        LeanTween.move(gameObject, closedPosition, GlideTime).setEaseOutCubic();
    }

    public override void Toggle()
    {
        LeanTween.cancel(gameObject);
        if (IsOpen())
        {
            Close();
        }
        else
        {
            Open();
        }
    }

    /// <summary>Instantly closes the menu</summary>
    public void InstantlyClose()
    {
        LeanTween.cancel(gameObject);
        transform.position = closedPosition;
        if (DeactivateOnClose) gameObject.SetActive(false);
    }

    /// <summary>Set the Menu's glide amount based on its EUIPosition</summary>
    private Vector2 CalculateClosedPosition()
    {
        return menuPosition switch
        {
            MenuPosition.Bottom =>    new Vector2(openPosition.x, 0),
            MenuPosition.Top => new Vector2(openPosition.x, menuBounds.height + ((RectTransform)Canvas.transform).rect.height),
            MenuPosition.Left =>   new Vector2(-menuBounds.width, openPosition.y),
            MenuPosition.Right =>  new Vector2(((RectTransform)Canvas.transform).rect.width, openPosition.y),
            _ => Vector2.zero,
        };
    }

    /// <summary>Calculates if its basically at the closed position, duh</summary>
    private bool BasicallyAtClosedPosition()
    {
        return transform.position.IsBasicallyEqualTo(closedPosition);
    }
}
