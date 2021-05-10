using UnityEngine;

namespace Draw_Climber_Virtual_Planet.Scripts.Game_Mechanics
{
    public class PlayerMovement : MonoBehaviour
    {
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
                var currentPosition = transform.position;
                
                var desiredPosition = currentPosition + Vector3.right * Speed();

                var newPosition = Vector3.Lerp(currentPosition, desiredPosition, acceleration);
                
                _rb.MovePosition(newPosition);
            }
        }

        private float Speed()
        {
            var angularVelocity = _rotateLeg.RotateSpeed * 2 * Mathf.PI;
            var speed = angularVelocity * _legsContacts.FurthestDistance;
            return speed;
        }

        private bool IsOnContact()
        {
            if (_legsContacts == null)
                return false;

            var contact = _legsContacts.Colliding;

            return contact;
        }
    }
}