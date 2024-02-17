using PokeBattler.Common;
using PokeBattler.Common.Extensions;
using System;
using UnityEngine;

namespace PokeBattler.Unity
{
    [AddComponentMenu("Poke Battler/Move To")]
    public class MoveToBehaviour : MonoBehaviour
    {
        static GameObject instanceOnCursor = null;

        #region Properties

        public static GameObject InstanceOnCursor { get => instanceOnCursor; }

        private Transform _originalParent;
        [SerializeField]
        private bool enableOnStart = true;
        [SerializeField]
        [Description("How far away from the ground the object should be, relative to the camera, when it's snapped to the cursor")]
        private float distanceTowardsCamera = 1f;
        public Vector3 Offset = Vector3.zero;
        [SerializeField]
        private bool useRectTransformPositioning = false;
        public bool ShouldLerpToPosition = false;
        public float LerpPercentage = 0.1f;

        Mode mode = Mode.Cursor;

        public Vector3 TargetPosition { get; private set; }
        public Transform Target { get; private set; }
        public bool IsSnappingToTarget => Target != _originalParent && mode == Mode.Target;

        #endregion

        void Start()
        {
            _originalParent = transform.parent;
            if (enableOnStart)
                MoveTo(Target);
        }

        void Update()
        {
            switch (mode)
            {
                case Mode.Cursor:
                    MoveToCursor();
                    break;
                case Mode.Target:
                    MoveToTarget();
                    break;
                default:
                    break;
            }
        }

        public void MoveTo(Transform newTarget, bool useOffset = true)
        {
            mode = Mode.Target;
            Target = newTarget;
            if (Target == null)
                return;

            if (Target == transform)
            {
                Debug.Log("Can't snap to self", gameObject);
                throw new Exception("Can't snap to self");
            }
            if (useRectTransformPositioning)
            {
                transform.SetParent(Target);
                SetPosition(Vector3.zero, useOffset);
                return;
            }
            SetPosition(Target.transform.position, useOffset);
        }

        void SetPosition(bool useOffset = true) => SetPosition(TargetPosition, useOffset);
   
        void SetPosition(Vector3 newPosition, bool useOffset = true)
        {
            TargetPosition = useOffset ? newPosition + Offset : newPosition;
            if (useRectTransformPositioning)
            {
                Vector3 localPos;
                if (ShouldLerpToPosition)
                {
                    var newPos = Vector3.Lerp(transform.RectTransform().anchoredPosition, TargetPosition, LerpPercentage);
                    

                    transform.RectTransform().anchoredPosition = newPos;
                    localPos = transform.RectTransform().localPosition;
                    var zLerp = Mathf.Lerp(localPos.z, TargetPosition.z, LerpPercentage);
                    if (localPos.IsBasicallyEqualTo(TargetPosition))
                        transform.RectTransform().localPosition = TargetPosition;
                    else
                        transform.RectTransform().localPosition = new Vector3(localPos.x, localPos.y, zLerp);
                    return;
                }
                transform.RectTransform().anchoredPosition = newPosition;
                localPos = transform.RectTransform().localPosition;
                transform.RectTransform().localPosition = new Vector3(localPos.x, localPos.y, newPosition.z);
                return;
            }
            if (ShouldLerpToPosition)
            {
                transform.position = Vector3.Lerp(transform.position, TargetPosition, LerpPercentage);
                return;
            }
            transform.position = TargetPosition;
        }

        public void MoveTo(Vector3 worldPosition, bool useOffset = true)
        {
            transform.SetParent(null);
            if (ShouldLerpToPosition)
            {
                TargetPosition = worldPosition;
                SetPosition(useOffset);
                return;
            }
            transform.position = worldPosition;
            SetPosition(useOffset);
        }

        public void MoveToTarget()
        {
            MoveTo(Target);
        }

        public void MoveToOrigin()
        {
            MoveTo(_originalParent);
        }

        public void MoveToCursor()
        {
            if (instanceOnCursor != null && instanceOnCursor != gameObject)
            {
                Debug2.Log("Can't snap to cursor, another object is already snapping to it", LogLevel.Detailed, instanceOnCursor);
                return;
            }
            if (mode == Mode.Target && useRectTransformPositioning)
            {
                transform.SetParent(null);
                transform.position = Target.transform.TransformPoint(Target.transform.RectTransform().localPosition);
            }
            mode = Mode.Cursor;
            instanceOnCursor = gameObject;
            var cursorPosition = Camera.main.WorldCursorButALittleTowardsTheCamera(distanceTowardsCamera, transform);
            MoveTo(cursorPosition, false);
        }

        public void ReleaseCursor()
        {
            instanceOnCursor = null;
            mode = Mode.Target;
        }

        public bool IsOnCursor()
        {
            return instanceOnCursor != null && instanceOnCursor.gameObject == gameObject;
        }

        /// <summary>
        /// Swaps snapping to the target and the Behaviours original parent transform
        /// </summary>
        public void SwapMoveTarget()
        {
            if (IsSnappingToTarget)
            {
                MoveTo(Target);
                return;
            }
            MoveTo(Target);
        }

        public void Enable()
        {
            if (Target != transform) Target.transform.SetParent(transform);
        }

        public void Disable()
        {
            Target.transform.SetParent(_originalParent);
        }

        public void Toggle()
        {
            if (IsSnappingToTarget)
                Disable();
            else
                Enable();
        }

        public enum Mode
        {
            Cursor,
            Target,
        }
    }
}
