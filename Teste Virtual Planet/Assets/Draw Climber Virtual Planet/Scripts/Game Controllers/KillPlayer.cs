using System;
using UnityEngine;

namespace Draw_Climber_Virtual_Planet.Scripts.Game_Controllers
{
    /// <summary>
    /// Kills the player when he leaves the play area 
    /// </summary>
    
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
