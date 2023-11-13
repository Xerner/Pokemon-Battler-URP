using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Modified version of code from
// https://images.squarespace-cdn.com/content/v1/58c47c61c534a58f938af1a7/1624837202800-9M10GCYCHDK7WQZBOECS/Input+Manager+Complete+Script.png?format=500w
public class InputManager : MonoBehaviour
{
    // InputActions refuses to be drawn in the Inspector. 
    // Even if a custom PropertyDrawer is implemented
    // So this property is used instead
    /// <summary>For drawing to the Editor only. See InputActionInfoDrawer</summary>
    public InputActionsInfo info;
    static InputActions _inputActions;
    public static InputActionMap CurrentActionMap;
    public static event Action<InputActionMap> ActionMapChange;

    public static InputActions InputActions
    {
        get 
        {
            if (_inputActions == null) _inputActions = new InputActions();
            return _inputActions;
        } 
    }

    void Awake() 
    {
        SwitchActionMap(InputActions.Main);
    }

    public static void SwitchActionMap(InputActionMap actionMap) 
    {
        if (actionMap.enabled)
            return;

        InputActions.Disable();
        ActionMapChange?.Invoke(actionMap);
        actionMap.Enable();
        CurrentActionMap = actionMap;
    }

    [Serializable]
    public class InputActionsInfo {}
}
