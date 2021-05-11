using UnityEngine;

namespace Draw_Climber_Virtual_Planet.Scripts.Game_Controllers
{
    /// <summary>
    /// Controls the aspect ratio of the camera on webgl platform
    /// </summary>
    public class WebGlCameraAspectController : MonoBehaviour
    {
        /// <summary>
        /// The cameras to adjust
        /// </summary>
        [SerializeField] private Camera[] cameras;

        private float _targetAspect = 0.5f;

        private void Update()
        {
#if UNITY_WEBGL
            var windowAspect =  (float) Screen.width /  Screen.height;
            var scaleHeight = windowAspect / _targetAspect;
            var scaleWidth = 1f / scaleHeight;

            if (scaleHeight < 1f)
            {
                AdjustHeight(scaleHeight);
            }
            else
            {
                AdjustWidth(scaleWidth);
            }
#endif
        }

        /// <summary>
        /// Adjusts the height of the cameras
        /// </summary>
        /// <param name="height"></param>
        private void AdjustHeight(float height)
        {
            foreach (var t in cameras)
            {
                var rect = t.rect;
                rect.width = 1f;
                rect.height = height;
                rect.x = 0;
                rect.y = (1f - height) / 2f;

                t.rect = rect;
            }
        }

        /// <summary>
        /// Adjusts the width of the cameras
        /// </summary>
        /// <param name="width"></param>
        private void AdjustWidth(float width)
        {
            foreach (var t in cameras)
            {
                var rect = t.rect;
                rect.width = width;
                rect.height = 1f;
                rect.x = (1f - width) / 2f;
                rect.y = 0;

                t.rect = rect;
            }
        }
    }
}
