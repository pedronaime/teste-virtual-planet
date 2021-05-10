using System;
using UnityEngine;

namespace Draw_Climber_Virtual_Planet.Scripts.Game_Controllers
{
    public class KillPlayer : MonoBehaviour
    {
        public static event Action PlayerDied;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
                PlayerDied?.Invoke();
        }
    }
}
