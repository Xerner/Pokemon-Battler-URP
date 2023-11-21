using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using static UnityEngine.InputSystem.InputAction;

namespace Poke.Unity
{
    public class CameraManager : MonoBehaviour
    {
        public static CameraManager Instance { get; private set; }

        static readonly int arenaColumns = 3;

        [SerializeField] Transform cameraRig;
        [SerializeField] ArenaBehaviour activeArena;
        [SerializeField] ArenaBehaviour[] arenas = new ArenaBehaviour[8];
        [Header("Camera Movement Bindings")]
        [SerializeField] InputActionReference MoveInput;
        int activeArenaIndex;
        int tweenID;

        void OnValidate()
        {
            if (cameraRig == null) 
                Debug.LogError("Camera Rig is null on CameraManager", gameObject);
        }

        void Start()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }
            else
            {
                Instance = this;
            }
            StartCoroutine(InitialMove());
        }

        IEnumerator InitialMove()
        {
            yield return new WaitForSeconds(1f);
            MoveInput.action.performed += Move;
            MoveToArena(activeArena);
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

        private void MoveToArena(ArenaBehaviour arena)
        {
            LTDescr lTDescr = LeanTween.move(cameraRig.gameObject, arena.transform, 0.5f);
            Debug2.Log($"Moving Camera to {arena.transform.position}", LogLevel.Detailed, gameObject);
            lTDescr.setEaseOutCubic();
            tweenID = lTDescr.id;
        }

        private bool InBounds(int index)
        {
            return index >= 0 && index < 8;
        }
    }
}
