using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    private static readonly int arenaColumns = 3;

    [SerializeField] private Arena activeArena;

    [SerializeField] private Arena[] arenas = new Arena[8];
    [SerializeField] private InputManager inputs;
    private Keyboard keyboard;
    private int activeArenaIndex;
    private int tweenID;

    private void Start()
    {
        keyboard = InputSystem.GetDevice<Keyboard>();
        MoveToArena(activeArena);
    }

    private void Update()
    {
        if (keyboard.upArrowKey.wasPressedThisFrame) Move(Direction.Up);
        if (keyboard.downArrowKey.wasPressedThisFrame) Move(Direction.Down);
        if (keyboard.leftArrowKey.wasPressedThisFrame) Move(Direction.Left);
        if (keyboard.rightArrowKey.wasPressedThisFrame) Move(Direction.Right);
    }

    public void Move(Direction direction)
    {
        int newArenaIndex;
        switch (direction)
        {
            case Direction.Up:
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
            case Direction.Down:
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
            case Direction.Left:
                newArenaIndex = activeArenaIndex - 1;
                break;
            case Direction.Right:
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

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}
