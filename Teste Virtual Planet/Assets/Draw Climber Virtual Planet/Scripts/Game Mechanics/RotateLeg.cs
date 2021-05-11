using UnityEngine;

namespace Draw_Climber_Virtual_Planet.Scripts.Game_Mechanics
{
    /// <summary>
    /// Rotates the players legs
    /// </summary>
    public class RotateLeg : MonoBehaviour
    {
        /// <summary>
        /// Number of rotations per second of the legs
        /// </summary>
        [SerializeField] private float rotationPerSecond;

        public float RotationPerSecond => rotationPerSecond;

        private void Update()
        {
            transform.Rotate(-Vector3.up, rotationPerSecond * Time.deltaTime * 360);
        }
    }
}
