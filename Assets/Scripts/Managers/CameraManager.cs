using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using static UnityEngine.InputSystem.InputAction;

public class CameraManager : PlayerInputConsumer
{
    public static CameraManager Instance { get; private set; }
    
    static readonly int arenaColumns = 3;
    [SerializeField] Arena activeArena;
    [SerializeField] Arena[] arenas = new Arena[8];
    [Header("Camera Movement Bindings")]
    [SerializeField] InputAction inputActionMove;
    //private Keyboard keyboard;
    int activeArenaIndex;
    int tweenID;

    private void Start()
    {
        if (Instance != null && Instance != this) {
            Destroy(this);
            return;
        } else {
            Instance = this;
        }
        //keyboard = InputSystem.GetDevice<Keyboard>();
        MoveToArena(activeArena);
        SubscribePlayerInput(inputActionMove, new Action<CallbackContext>[] { Move });
    }

    //private void Update()
    //{
    //    if (keyboard.upArrowKey.wasPressedThisFrame) Move(Direction.Up);
    //    if (keyboard.downArrowKey.wasPressedThisFrame) Move(Direction.Down);
    //    if (keyboard.leftArrowKey.wasPressedThisFrame) Move(Direction.Left);
    //    if (keyboard.rightArrowKey.wasPressedThisFrame) Move(Direction.Right);
    //}

    public void OnArrowKeyPressed(CallbackContext context) {
        if (context.started) {
            var inputControl = (KeyControl)context.control;
            Move(inputControl.keyCode);
        }
    }

    public void Move(CallbackContext context) => Move(((KeyControl)context.control).keyCode);

    public void Move(Key key)
    {
        Debug2.Log("Moving camera: " + key, LogLevel.Detailed);
        int newArenaIndex;
        switch (key)
        {
            case Key.UpArrow:
                switch (activeArenaIndex)
                {
                    case 4:
                    case 5:
                        newArenaIndex = activeArenaIndex - 2;
                        break;
                    case 6:
                        newArenaIndex = activeArenaIndex - 5;
                        break;
                    default:
                        newArenaIndex = activeArenaIndex - arenaColumns;
                        break;
                }
                break;
            case Key.DownArrow:
                switch (activeArenaIndex)
                {
                    case 2:
                    case 3:
                        newArenaIndex = activeArenaIndex + 2;
                        break;
                    case 1:
                        newArenaIndex = activeArenaIndex + 5;
                        break;
                    default:
                        newArenaIndex = activeArenaIndex + arenaColumns;
                        break;
                }
                break;
            case Key.LeftArrow:
                newArenaIndex = activeArenaIndex - 1;
                break;
            case Key.RightArrow:
                newArenaIndex = activeArenaIndex + 1;
                break;
            default:
                throw new System.Exception("Invalid camera direction");
        }
        if (InBounds(newArenaIndex))
        {
            if (LeanTween.isTweening(gameObject)) LeanTween.cancel(tweenID);
            MoveToArena(arenas[newArenaIndex]);
            activeArenaIndex = newArenaIndex;
            activeArena = arenas[activeArenaIndex];
        }
    }

    private void MoveToArena(Arena arena)
    {
        LTDescr lTDescr= LeanTween.move(gameObject, new Vector2(arena.transform.position.x, arena.transform.position.y), 0.5f);
        lTDescr.setEaseOutCubic();
        tweenID = lTDescr.id;
    }

    private bool InBounds(int index)
    {
        return index >= 0 && index < 8;
    }
}
