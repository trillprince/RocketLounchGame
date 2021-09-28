using UnityEngine;

namespace Common.Scripts.Input
{
    public class Utils : MonoBehaviour
    {
        public static Vector3 ScreenToWorld(UnityEngine.Camera camera, Vector3 position)
        {
            position.z = camera.nearClipPlane;
            return camera.ScreenToViewportPoint(position);
        }
    }
}