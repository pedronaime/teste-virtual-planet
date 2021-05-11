using UnityEngine;

namespace Draw_Climber_Virtual_Planet.Scripts.Game_Mechanics
{
    /// <summary>
    /// Moves the player based on the size and speed of the legs
    /// </summary>
    
    public class PlayerMovement : MonoBehaviour
    {
        /// <summary>
        /// The acceleration of the player 
        /// </summary>
        [SerializeField] private float acceleration;
        
        private Rigidbody _rb;
        private HandleLegContact _legsContacts;
        private RotateLeg _rotateLeg;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _legsContacts = GetComponent<HandleLegContact>();
            _rotateLeg = GetComponentInChildren<RotateLeg>();
        }

        private void FixedUpdate()
        {
            if (IsOnContact())
            {
                Move();
            }
        }

        /// <summary>
        /// Moves the player
        /// </summary>
        /// <returns></returns>
        private void Move()
        {
            var currentPosition = transform.position;
                
            var desiredPosition = currentPosition + Vector3.right * Speed();

            var newPosition = Vector3.Lerp(currentPosition, desiredPosition, acceleration);
                
            _rb.MovePosition(newPosition);
        }

        /// <summary>
        /// Calculates the speed of the player based upon the size and angular velocity of the legs
        /// </summary>
        /// <returns></returns>
        private float Speed()
        {
            var angularVelocity = _rotateLeg.RotationPerSecond * 2 * Mathf.PI;
            var speed = angularVelocity * _legsContacts.FurthestDistance;
            return speed;
        }

        /// <summary>
        /// Verifies if the leg is on contact with the scenario
        /// </summary>
        /// <returns></returns>
        private bool IsOnContact()
        {
            if (_legsContacts == null)
                return false;

            var contact = _legsContacts.Colliding;

            return contact;
        }
    }
}