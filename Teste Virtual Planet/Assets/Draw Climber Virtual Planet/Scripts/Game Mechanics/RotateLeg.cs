using UnityEngine;

namespace Draw_Climber_Virtual_Planet.Scripts.Game_Mechanics
{
    public class RotateLeg : MonoBehaviour
    {
        [SerializeField] private float rotateSpeed;

        public float RotateSpeed => rotateSpeed;

        private void Update()
        {
            transform.Rotate(-Vector3.up, rotateSpeed * Time.deltaTime * 360);
        }
    }
}
