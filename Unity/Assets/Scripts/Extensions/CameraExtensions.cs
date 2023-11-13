using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Poke.Core
{
    public static class CameraExtensions
    {
        public static Transform CameraRig(this Camera camera)
        {
            var rig = camera.transform.parent;
            return rig;
        }

        public static RaycastHit[] CursorRaycastHits(this Camera camera)
        {
            Vector2 mouseScreenPosition = Mouse.current.position.value;
            if (mouseScreenPosition.x < 0 || mouseScreenPosition.y < 0)
            {
                return null;
            }
            RaycastHit[] hits = Physics.RaycastAll(camera.ScreenPointToRay(mouseScreenPosition));
            return hits;
        }

        public static Vector2 CenterOfScreen(this Camera _)
        {
            return new(Screen.width / 2, Screen.height / 2);
        }

        public static Vector3 WorldCursorButALittleTowardsTheCamera(this Camera camera, float distanceTowardsCamera, Transform[] ignoreTransforms)
        {
            var hits = camera.WorldCursorButALittleTowardsTheCamera();
            RaycastHit ground = hits.FirstOrDefault(hit => !ignoreTransforms.Any(transform  => hit.transform == transform));
            return camera.WorldCursorButALittleTowardsTheCamera(distanceTowardsCamera, ground);
        }

        public static Vector3 WorldCursorButALittleTowardsTheCamera(this Camera camera, float distanceTowardsCamera, Transform ignoreTransform)
        {
            var hits = camera.WorldCursorButALittleTowardsTheCamera();
            RaycastHit ground = hits.FirstOrDefault(hit => hit.transform != ignoreTransform);
            return camera.WorldCursorButALittleTowardsTheCamera(distanceTowardsCamera, ground);
        }

        public static RaycastHit[] WorldCursorButALittleTowardsTheCamera(this Camera camera)
        {
            Vector2 mouseScreenPosition = Mouse.current.position.value;
            if (mouseScreenPosition.x < 0 || mouseScreenPosition.y < 0)
            {
                Vector2 centerOfScreen = camera.CenterOfScreen();
                mouseScreenPosition = centerOfScreen;
            }
            RaycastHit[] hits = Physics.RaycastAll(camera.ScreenPointToRay(mouseScreenPosition));
            return hits;
        }

        public static Vector3 WorldCursorButALittleTowardsTheCamera(this Camera camera, float distanceTowardsCamera, RaycastHit ground)
        {
            Vector3 groundPosition = ground.transform != null ? ground.point : Vector3.zero;
            Vector3 distanceFromCameraToGround = (camera.transform.position - groundPosition).normalized * distanceTowardsCamera;
            distanceFromCameraToGround = (camera.transform.position - groundPosition) - distanceFromCameraToGround;
            Vector3 newPosition = camera.transform.position - distanceFromCameraToGround;
            return newPosition;
        }
    }
}
