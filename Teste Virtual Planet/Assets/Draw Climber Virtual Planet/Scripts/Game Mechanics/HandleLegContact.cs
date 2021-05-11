using UnityEngine;

namespace Draw_Climber_Virtual_Planet.Scripts.Game_Mechanics
{
    /// <summary>
    /// Handles the collision between the leg and the scenario
    /// </summary>
    
    public class HandleLegContact : MonoBehaviour
    {
        public float FurthestDistance { get; private set; }

        public bool Colliding { get; private set; }

        private void OnCollisionEnter(Collision other)
        {
            if (IsLeg(other))
            {
                Colliding = true;
            }
        }
        
        private void OnCollisionExit(Collision other)
        {
            Debug.Log("Leg left Contact");
            Colliding = false;
        }

        /// <summary>
        /// Verifies if the collision is between the leg and another object and sets the furthest contact distance
        /// </summary>
        /// <param name="collision"></param>
        /// <returns></returns>
        private bool IsLeg(Collision collision)
        {
            var position = transform.position;
            
            for (int i = 0; i < collision.contactCount; i++)
            {
                var point = collision.GetContact(i).point;
                var distance = Vector3.Distance(point, position);

                if (distance > FurthestDistance)
                {
                    FurthestDistance = distance;
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
