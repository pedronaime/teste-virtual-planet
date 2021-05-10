using UnityEngine;

namespace Draw_Climber_Virtual_Planet.Scripts.Game_Mechanics
{
    public class HandleLegContact : MonoBehaviour
    {
        private float _furthestDistance;
        public float FurthestDistance => _furthestDistance;

        private Vector3 _furthestPoint;

        public Vector3 FurthestPoint => _furthestPoint;

        private bool _colliding;
        public bool Colliding => _colliding;

        private void OnCollisionEnter(Collision other)
        {
            if (IsLeg(other))
            {
                _colliding = true;
            }
        }
        
        private void OnCollisionExit(Collision other)
        {
            Debug.Log("Leg left Contact");
            _colliding = false;
        }

        private bool IsLeg(Collision collision)
        {
            var position = transform.position;
            
            for (int i = 0; i < collision.contactCount; i++)
            {
                var point = collision.GetContact(i).point;
                var distance = Vector3.Distance(point, position);

                if (distance > _furthestDistance)
                {
                    _furthestPoint = point;
                    _furthestDistance = distance;
                }
                
                if (collision.GetContact(i).thisCollider.CompareTag("Leg"))
                {
                    Debug.Log("Leg in Contact");
                    return true;
                }
            }

            return false;
        }
    }
}
