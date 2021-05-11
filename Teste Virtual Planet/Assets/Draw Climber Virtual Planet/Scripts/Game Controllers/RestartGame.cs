using UnityEngine;
using UnityEngine.SceneManagement;

namespace Draw_Climber_Virtual_Planet.Scripts.Game_Controllers
{
    /// <summary>
    /// Reload the scene when the player dies
    /// </summary>
    
    public class RestartGame : MonoBehaviour
    {
        private void Awake()
        {
            KillPlayer.PlayerDied += Restart;
        }

        private void Restart()
        {
            SceneManager.LoadScene(0);
        }
    }
}
