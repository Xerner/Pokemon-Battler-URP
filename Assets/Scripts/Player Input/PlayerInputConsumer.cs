using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public abstract class PlayerInputConsumer : MonoBehaviour
{
    //protected void InitializePlayerInput((string, Action<InputAction.CallbackContext>)[] actionTuple) {
    //    var playerInput = GetComponent<PlayerInput>();
    //    if (playerInput.isActiveAndEnabled) {
    //        foreach (var tuple in actionTuple) {
    //            var action = playerInput.actions.FindAction(tuple.Item1);
    //            if (action == null) Debug2.Log($"Player Input Action is null\nAction: {tuple.Item1}\tAction Map: {playerInput.actions}");
    //            else action.performed += tuple.Item2;
    //        }
    //    }
    //}

    /// <summary>
    /// Initialize the PlayerInput components default ActionMap with an InputAction 
    /// that is contructed in the Editor, and a list of callbacks for that action
    /// </summary>
    /// <param name="callbacks">The list of functions to call when the action is performed</param>
    protected void InitializePlayerInput(InputAction inputAction, Action<InputAction.CallbackContext>[] callbacks) {
        var playerInput = GetComponent<PlayerInput>();
        if (playerInput.isActiveAndEnabled) {
            var action = playerInput.actions.FindAction(inputAction.name);
            // Make the action if it did not already exist
            playerInput.currentActionMap.Disable();
            if (action == null) action = playerInput.currentActionMap.AddAction(inputAction.name, inputAction.type);
            playerInput.currentActionMap.Enable();
            if (inputAction.bindings.Count == 0) Debug.LogError($"{gameObject.name} has no bindings for its PlayerInput!", gameObject);
            foreach (var binding in inputAction.bindings) action.AddBinding(binding);
            foreach (var callback in callbacks) action.performed += callback;
        }
    }
}
