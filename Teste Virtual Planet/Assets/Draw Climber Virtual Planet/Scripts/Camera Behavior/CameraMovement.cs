using UnityEngine;

namespace Draw_Climber_Virtual_Planet.Scripts.Camera_Behavior
{
    /// <summary>
    /// A basic follow camera with a dampening effect 
    /// </summary>
    
    public class CameraMovement : MonoBehaviour
    {
        /// <summary>
        /// The player transform
        /// </summary>
        [SerializeField] private Transform player;
        
        private Vector3 _cameraOffset;
        
        private void Start()
        {
            _cameraOffset = player.position - transform.position;
        }

        private void Update()
        {
            var currentPosition = transform.position;
            var desiredPosition = player.position - _cameraOffset;
            var newPosition = Vector3.Lerp(currentPosition, desiredPosition, 0.1f);

            transform.position = newPosition;
        }
    }
}

