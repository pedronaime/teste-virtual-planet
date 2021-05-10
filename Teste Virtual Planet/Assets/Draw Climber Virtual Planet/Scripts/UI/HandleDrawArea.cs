using UnityEngine;
using UnityEngine.EventSystems;

namespace Draw_Climber_Virtual_Planet.Scripts.UI
{
    public class HandleDrawArea : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, IPointerUpHandler
    {
        
        public static bool CanDraw;

        private void Awake()
        {
            Time.timeScale = 0;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Time.timeScale = 0.01f;
            CanDraw = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Time.timeScale = 1f;
            CanDraw = false;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Time.timeScale = 1f;
            CanDraw = false;
        }
    }
}
